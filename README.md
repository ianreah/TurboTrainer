TurboTrainer
============

[![Build status](https://ci.appveyor.com/api/projects/status?id=v5e087xb0gs3gwu8)](https://ci.appveyor.com/project/turbotrainer)

TurboTrainer is a really simple application that loads in a GPX file and plays it back in real time, displaying the route’s current gradient on the screen.

![TurboTrainer screenshot](http://ianreah.com/img/post-2013-11-19-turbotrainer-wpf.png)

The idea is to be able to use it with an exercise bike or turbo trainer to make your workouts a bit more realistic by following an actual outdoor route. You'd just watch the display and adjust the trainer's resistance according the displayed gradient.

I made it as an exercise in using [Xamarin](http://xamarin.com/) and [ReactiveUI](http://www.reactiveui.net/) to write cross-platform MVVM. The project contains a WPF app and an Android app sharing practically all of the same code. There's a [blog post](http://ianreah.com/2013/11/24/cross-platform-mvvm-with-reactiveui-and-xamarin.html) if you want to read more about this.
