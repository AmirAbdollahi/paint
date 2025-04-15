# Paint Application (C#)

Welcome to my **Paint** application — one of the first major challenges I tackled when I started learning **C#**. This project holds a special place in my journey as a developer, and it's a fun attempt to simulate the basic functionality of the classic **MS Paint** application.

## Features

- Draw lines, rectangles, ellipses, and freehand shapes
- Change pen color and thickness
- Fill shapes with colors
- Erase content
- Clear canvas
- Save your drawing

## Screenshots

![image](https://github.com/user-attachments/assets/5e6f9663-c5de-4300-a16e-feb04ae520a3)

## Demo GIF

![PaintDemo_2](https://github.com/user-attachments/assets/15594f7c-c27b-4280-841a-72b8a10cff40)

---

## Project Goals

The main goal of this project was to simulate the basic functionalities of the classic Microsoft Paint application using **C#** and **WinForms**. The focus was on learning graphical programming concepts, handling user inputs, and building an interactive desktop UI. It served as a hands-on project to understand:

- Event-driven programming
- Drawing graphics on a canvas
- Real-time mouse interaction
- Building user-friendly interfaces

---

## Architecture Overview

The project follows a simple structure, designed for learning and experimentation rather than scalability. Here's an overview of how it's built:

- **Windows Forms (WinForms)**: Used for the UI and drawing surface.
- **Main Form (UI Layer)**: Contains buttons, color selectors, and the drawing canvas.
- **Drawing Logic**: Handles mouse events (Down, Move, Up) to draw on the canvas using the `Graphics` object.
- **State Management**: Keeps track of selected tool, color, brush size, and drawing mode.

---

## Technical Details

- **Language**: C#
- **Framework**: .NET Framework (WinForms)
- **Drawing Surface**: `PictureBox` or a custom panel used to handle drawing.
- **Rendering**: Uses `Graphics` class for shapes and freehand lines.
- **Mouse Events**: `MouseDown`, `MouseMove`, and `MouseUp` events are used for drawing actions.
- **Brush Settings**: Allows changing color and pen width dynamically.
- **Save Feature**: Exports the drawing as an image file using `Bitmap` and `SaveFileDialog`.

---

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/AmirAbdollahi/paint.git
   ```

2. Open the solution file (`Paint.sln`) in **Visual Studio**.

3. Build and run the project.

## Motivation

This was a self-challenge project during my early days of learning C#. I wanted to explore how to handle **graphics**, **user input**, and **WinForms** events. It helped me gain a deeper understanding of Windows desktop development and UI rendering.

## What I Learned

- Drawing on `Graphics` objects
- Managing user interactions like mouse movements
- Handling UI components and events in WinForms
- Basics of color and shape manipulation in a GUI

## Acknowledgments

Inspired by the classic **Microsoft Paint** and my curiosity about how drawing tools work behind the scenes.

## Contribution

Feel free to explore, fork, or improve this project. I'd love to see what you come up with!
