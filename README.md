<div align="center">
	<h1>Reloaded II: DInput Please Cooperate!</h1>
	<img src="https://i.imgur.com/BjPn7rU.png" width="150" align="center" />
	<br/> <br/>
	<strong>I'm out of ideas.</strong>
	<p>Please submit a PR with a cringeworthy line to put here..</p>
    <b>Id: Reloaded.Universal.DInputPleaseCooperate</b>
</div>

# About This Project

Ever play a game, try to take a screenshot ... and nothing happens? Game's blocking your hotkeys?

Here's a universal mod for games using DirectInput that should hopefully unblock your precious keys.

# Compatibility

In theory this should work with any DirectInput application.

In practice, I have tested it with the following:

- Sonic Riders  
- Eiyuu Densetsu: Ao no Kiseki

# How it Works

This is a very simple mod that works by hooking the `SetCooperativeLevel` function of DirectInput. 

It unsets the `Exclusive` flag that prevents other applications from reading the keyboard as well as the `NoWinKey` flag.

The `NoWinKey` flag is a bit misleading. It actually does a bit more than blocking the Windows key, such as blocking hotkeys registered through Windows API's `RegisterHotkey` function. Many applications, such as the popular screenshot utility ShareX use that API for receiving hotkeys.

# Acknowledgements

Icon: [Controller by iconfield from Noun Project](https://thenounproject.com/browse/icons/term/controller/).  