# SpeedBall 
---
<p align="center">
  <img src="https://github.com/user-attachments/assets/42ad9bca-5b37-44b5-8c3d-0e3f613430bf" width="350" />
</p>

# User Stories
---

  | ![Image 1](https://github.com/user-attachments/assets/1948d527-da9a-491b-bd07-61564753a7d2) | ![Image 2](https://github.com/user-attachments/assets/fc52fb3d-5493-43b6-9c16-f82fbc710f96) |
  |---|---|

# Backlog
---

### GRAPHICS  
    Design for cars, ball, goal posts, boosters, buttons, numbers, arrows, background, menu, titles.

### MENU IMPLEMENTATION  
    The menu should highlight options such as:
    - PLAY button – Choose to play 1V1 or MULTIPLAYER  
    - CUSTOMIZE CAR button  
    - SETTINGS button  
    - USER STATISTICS button  
    - EXIT GAME button

### SETTINGS MENU IMPLEMENTATION  
    Users can change the resolution, the volume, and toggle fullscreen mode.

### USER STATISTICS IMPLEMENTATION  
    Users can check their stats: total played games, total play time, wins and losses.

### CAR CUSTOMIZATION MENU IMPLEMENTATION  
    A user can choose a car from a predefined list (different colors and styles).

### PAUSE MENU IMPLEMENTATION  
    The pause menu should include:
    - HOME button  
    - RESTART button  
    - RESUME button

### CAR MOVEMENT  
    - Player 1: Move with **A & D**, jump with **N**, boost with **B**  
    - Player 2: Move with **← & →**, jump with **NumPad 1**, boost with **NumPad 2**

### BALL MOVEMENT  
    The ball moves upon collision with a car.

### PHYSICS  
    Defining game objects, environment limits, and object properties.

### 1V1 GAME MODE  
    Ensures two users can play locally on the same device.

### MULTIPLAYER GAME MODE – SERVER IMPLEMENTATION  
    - Server creation  
    - Create Room functionality  
    - Join Room functionality

### ANIMATIONS  
    - Car wheels move based on input  
    - Wheels are on fire when boosting (only if car is moving)
    
### AUDIO  
    - Background music (menu & in-game)  
    - Applause on goal  
    - Button click sounds  
    - Sounds for winning/losing  
    - Sound on ball collision

### BOOSTERS MANAGEMENT  
    Boosters appear during the game, are collected mid-air, and activated via the boost button.
    
### ROUND DURATION IMPLEMENTATION  
    The game ends when the round timer hits 0.  
    The player with the highest score wins.

# Source control using Git
---
- *branch creation*
  
  ![image](https://github.com/user-attachments/assets/ecd102c9-b20c-4a31-9f34-043b517ab4d2)
  Also, find all branches here 👉 [View Branches](https://github.com/Stefan-101/SpeedBall/branches)

- *merge/rebase*

  | ![Image 1](https://github.com/user-attachments/assets/9660ac2c-8f39-4bdb-8e68-d2f1c7dddb7a) | ![Image 2](https://github.com/user-attachments/assets/f11d3134-c678-45e8-acec-8a74e87667fd) |
  |:--:|:--:|

  Also, find all merges here 👉 [View Merges](https://github.com/Stefan-101/SpeedBall/network)

- *pull requests*
  
  | ![Image 1](https://github.com/user-attachments/assets/8ef39cd9-e5c1-4dc6-9fb0-23d096244507) | ![Image 2](https://github.com/user-attachments/assets/fe0f2b50-292b-402b-974b-8710f72b598b) |
  |:--:|:--:|

  Also, find all pull requests here 👉 [View Pull Requests](https://github.com/Stefan-101/SpeedBall/pulls?q=is%3Apr+is%3Aclosed)

# Automate tests
---
 Tests for car movement can be seen here 👉 [View Tests](https://www.youtube.com/watch?v=nsvnU43GhPQ&feature=youtu.be).
 Tests were made for checking:
   - boosters for car
   - jump functionality for car
   - car's moving (left or right)
     
# Reporting bugs and solving them with pull request
---

  | ![Image 1](https://github.com/user-attachments/assets/a98c21b2-d463-4883-b3d5-285c37830e68) | ![Image 2](https://github.com/user-attachments/assets/86912f38-efa3-4274-a48c-9ac11216b8a6) | ![Image 3](https://github.com/user-attachments/assets/ce0b9991-dc33-4bff-b088-04b3aa5dd141) |
  |:--:|:--:|:--:|

Also, find issues here 👉 [View Issues](https://github.com/Stefan-101/SpeedBall/issues)

# Code - the use of comments & ensuring code standards
---
  
  We follow clean and consistent code standards throughout the project, as reflected in the scripts from this repository.
  
  You can see the scripts [here](https://github.com/Stefan-101/SpeedBall/tree/master/Assets) (all the .cs files).

# Design patterns
---
- ***Singleton for Audio Manager*** 👉 [View Singleton for Audio Manager](https://github.com/Stefan-101/SpeedBall/blob/master/Assets/AudioManagerScript.cs)
- ***Singleton for User Statistics*** 👉 [View Singleton for Stats Manager](https://github.com/Stefan-101/SpeedBall/blob/master/Assets/StatsManager.cs)
  
# Prompt engineering - Documenting the use of AI tools during software development
---
***Used AI tools:***
---
- ChatGPT
- Claude.ai
- Deepseek
    
***What did we used these tools for?***
---

***1. Learning***

  - using *ChatGPT*, we learned about MonoBehaviour class in Unity: This information helped us to understand better the logic of every element in our code.
    
      <p align="center">
        <img src="https://github.com/user-attachments/assets/b94e22da-18cf-4fc9-b222-7f3ac14ec5b2" alt="MonoBehaviour" width="700">
      </p>
    
  - using *Claude.ai*, we learned about linking animations to objects in Unity: This information helped us ensure a great conectivity between the animations and the objects we wanted to apply the animation on, ensuring it's duration lasts as long as an event happens(example: pressing a certain key from keyboard).
    
      <p align="center">
        <img src="https://github.com/user-attachments/assets/856c7e66-8836-49bf-a4cf-a800d4156bf0" alt="Animation Linking">
      </p>

  - using *Deepseek*, we learned about certain essential methods in Unity: This information helped us to understand better the functionality of Unity. Also, it learned us how to use memory in an efficient way.
       
      <p align="center">
        <img src="https://github.com/user-attachments/assets/48a7ae6d-d109-47e3-af05-3214253e2161" alt="Unity Methods" width="550">
      </p>


***2. Implementing several parts of code***
---
Some examples:
   
 - trimming an audio in code with ChatGPT
  <p align="center">
    <img src="https://github.com/user-attachments/assets/2721035f-a3c2-4308-b267-2826e9a54d80" alt="Audio Trimming" width=600>
  </p>

- creating a dropdown for choosing the resolution in the settings menu with ChatGPT
  <p align="center">
    <img src="https://github.com/user-attachments/assets/b6a15ce9-70c4-4a30-b1af-2e44f8301ea4" alt="Resolution Dropdown" width=600>
  </p>

- implementing double jump for cars in the game with ChatGPT
  <p align="center">
    <img src="https://github.com/user-attachments/assets/18f0c6bf-d506-4288-9572-34f99a7f48d6" alt="Double Jump" width=700>
  </p>


We also provided the AI tools with screenshots from Unity and detailed context in our prompts, helping them better understand the structure and purpose of our project. This allowed for more accurate and relevant responses tailored to our specific development needs.
  <p align="center"> 
    <img src="https://github.com/user-attachments/assets/38ce1a60-e2cc-43b5-b4b5-73baaf1b33dd" witdh=400>
  </p>

***What did we learn from using AI in our project?***
---

- The best results come from asking **specific, clear, and context-rich questions**.  
- Complex problems require **step-by-step instructions** to get accurate and useful answers.  
- Providing **detailed information** helps the AI fully understand the request and generate better solutions.  
- Clear prompts save time.

***How prompt engineering helped our workflow***
---
Using prompt engineering significantly improved our development process by allowing us to:
- Quickly generate accurate and relevant code snippets without spending hours searching or debugging.  
- Clarify complex programming tasks by breaking them down into simple, step-by-step instructions.  
- Learn new concepts and best practices through targeted AI explanations and examples. 

# Demo
---
The offline demo can be seen on this link 👉 [View Demo]()

# Our team
---
The development of Speedball was made possible by a dedicated team made of:
- **Andrei Ștefan** - Our motion engineer: created the systems that give life and speed to the cars in Speedball.
- **Bejan Ștefan** - Our steady hand behind the servers and menus, dedicated to a flawless experience in game and careful testing.
- **Lumînăraru Ionuț-Andrei** - Our gameplay director: ensures the rhythm by managing the score, time and the ending of the rounds.
- **Onisie Andreea** - The artist and visual architect of the game: everything you see, hear and feel in Speedball wears her signature.
- **Stan David-Florin** - The engine behind the multiplayer: ensured stable connections, solid servers, a menu and scenes that work flawlessly.
