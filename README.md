# Multi Class Example

This project demonstrates how to create, inject, and use a factory class to provide instances of other classes.

## The Problem

We have multiple classes that implement a particular interface. We want to have access to these classes through dependency injection without injecting the concrete class or creating separate interfaces for each class.

## The Solution

Instead of trying to inject the individual classes, we inject a factory class that can create the classes that we need.

## Notes

Because the classes created by the factory class also require dependency injection, we make sure that we inject any needed classes into the factory so it can pass them on when it creates a class.
