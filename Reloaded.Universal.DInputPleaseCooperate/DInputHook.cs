using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X64;
using SharpDX.DirectInput;
using CallingConventions = Reloaded.Hooks.Definitions.X86.CallingConventions;

namespace Reloaded.Universal.DInputPleaseCooperate
{
    public unsafe class DInputHook
    {
        private IHook<SetCooperativeLevel> _setCooperativeLevelHook;

        /* Setup & Teardown */
        public DInputHook(IReloadedHooks _hooks)
        {
            var functionAddress = GetCooperativeLevelAddress();
            _setCooperativeLevelHook = _hooks.CreateHook<SetCooperativeLevel>(SetCooperativeLevelImpl, (long) functionAddress).Activate();
        }

        private void SetCooperativeLevelImpl(IntPtr thisPtr, IntPtr hwnd, CooperativeLevel flags)
        {
            flags &= (~CooperativeLevel.NoWinKey);
            flags &= (~CooperativeLevel.Exclusive);
            flags |= (~CooperativeLevel.NonExclusive);
            _setCooperativeLevelHook.OriginalFunction(thisPtr, hwnd, flags);
        }

        /// <summary>
        /// Gets the address of <see cref="SetCooperativeLevel"/> function.
        /// </summary>
        private unsafe void* GetCooperativeLevelAddress()
        {
            var adapter = new DirectInput();
            var devices = adapter.GetDevices(DeviceClass.All, DeviceEnumerationFlags.AttachedOnly);
            if (devices.Count <= 0) 
                return (void*) 0x0;
            
            using var joystick = new Joystick(adapter, devices[0].InstanceGuid);
            var vTable   = *(void**)joystick.NativePointer;
            var entry    = ((void**) vTable)[13]; // VTable index of SetCooperativeLevel
            return entry;
        }

        /* Functions */
        
        public void Suspend() => _setCooperativeLevelHook.Disable();
        public void Resume() => _setCooperativeLevelHook.Enable();
        public void Unload() => Suspend();

        /* Definitions */

        /// <summary>
        /// Establishes the cooperative level for this instance of the device.	
        /// </summary>
        /// <param name="thisPtr">Pointer to the "this" object.</param>
        /// <param name="arg0">Window handle to be associated with the device. This parameter must be a valid top-level window handle that belongs to the process. The window associated with the device must not be destroyed while it is still active in a DirectInput device.</param>
        /// <param name="arg1">Flags that describe the cooperative level associated with the device. The following flags are defined.</param>
        [Function(Hooks.Definitions.X64.CallingConventions.Microsoft)]
        [Hooks.Definitions.X86.Function(CallingConventions.Stdcall)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetCooperativeLevel(IntPtr thisPtr, IntPtr hwnd, CooperativeLevel flags);

    }
}
