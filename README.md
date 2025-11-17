# Coko Discord Bot
Coko Discord Bot is a multipurpose bot built specifically for the **"Empire of Coko"** Discord server.  
The project is currently **under active development** and follows a modular architecture to keep features organized, scalable, and easy to maintain.

## 🚀 **Overview**
The bot integrates several features designed to automate tasks and enhance the server experience. Its modular structure allows new functionalities to be added without interfering with the core system.

## 📦 **Current Modules**

### 🎵 **Daily Song**

This module handles the **Daily Song** feature.  
I wanted to recommend a new song every day, but often forgot or couldn’t remember whether a song had already been used.  
To solve this, the module uses:

- An internal timer
- A SQLite database
- A small Python script to add songs more easily

It automatically selects and posts a new song each day.
### 🧱 **Core**

The **Core** module provides shared utilities and common elements used across all other modules, without relying on external libraries.  
It contains:

- Shared configuration
- Reusable templates
- Utility functions

This module acts as the stable foundation of the project.

### 🤖 **App**

This is the main application module. It is responsible for:

- Handling the connection with Discord
- Managing and routing bot commands
- Coordinating interactions between modules
- Hosting small standalone features that don’t require their own module

It serves as the entry point and central orchestrator of the entire system.

### 🤖 **AI**

To allow more interaction for the bot outside normal functions, I wanted to add the ability to have normal chatting.
This module handles everything related to the connection with the AI wich is running locally.

- Handling the connection with AI
- Handling system prompts
- Handling contexts to have "memory" (Currently with a deprecated function)

It allows as brigde between the local AI and the discord chat.
## 🛠️ **Project Status**

The bot is still being actively developed, and new modules and features will be added over time.  
Feedback, suggestions, and contributions are always welcome.

## **Tech Stack**
- C# (.NET 9)
- DSharpPlus
- SQLite
- Python (utility scripts)
