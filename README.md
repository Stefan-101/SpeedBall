# SpeedBall 
---
<p align="center">
  <img src="https://github.com/user-attachments/assets/42ad9bca-5b37-44b5-8c3d-0e3f613430bf" width="200" />
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
    - **PLAY button** – Choose to play 1V1 or MULTIPLAYER  
    - **CUSTOMIZE CAR button**  
    - **SETTINGS button**  
    - **USER STATISTICS button**  
    - **EXIT GAME button**

### SETTINGS MENU IMPLEMENTATION  
    Users can change the resolution, the volume, and toggle fullscreen mode.

### USER STATISTICS IMPLEMENTATION  
    Users can check their stats: total played games, total play time, wins and losses.

### CAR CUSTOMIZATION MENU IMPLEMENTATION  
    A user can choose a car from a predefined list (different colors and styles).

### PAUSE MENU IMPLEMENTATION  
    The pause menu should include:
    - **HOME button**  
    - **RESTART button**  
    - **RESUME button**

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
    - **Create Room** functionality  
    - **Join Room** functionality

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

 
# Diagrame
---

# Source control cu Git
---
- *branch creation*
  
  ![image](https://github.com/user-attachments/assets/ecd102c9-b20c-4a31-9f34-043b517ab4d2)

- *merge/rebase*

| ![Image 1](https://github.com/user-attachments/assets/9660ac2c-8f39-4bdb-8e68-d2f1c7dddb7a) | ![Image 2](https://github.com/user-attachments/assets/f11d3134-c678-45e8-acec-8a74e87667fd) |
|:--:|:--:|

- *pull requests*
  
| ![Image 1](https://github.com/user-attachments/assets/8ef39cd9-e5c1-4dc6-9fb0-23d096244507) | ![Image 2](https://github.com/user-attachments/assets/fe0f2b50-292b-402b-974b-8710f72b598b) |
|:--:|:--:|

# Teste automate
---

# Raportare bug-uri si rezolvare cu pull request
---

| ![Image 1](https://github.com/user-attachments/assets/a98c21b2-d463-4883-b3d5-285c37830e68) | ![Image 2](https://github.com/user-attachments/assets/86912f38-efa3-4274-a48c-9ac11216b8a6) | ![Image 3](https://github.com/user-attachments/assets/ce0b9991-dc33-4bff-b088-04b3aa5dd141) |
|:--:|:--:|:--:|

  Also, find issues here 👉 [View Issues](https://github.com/Stefan-101/SpeedBall/issues)

# Comentarii cod si respectarea code standard
---
  We follow clean and consistent code standards throughout the project, as reflected in the scripts from this repository.

# Design patterns
---
- ***Singleton for Audio Manager*** 👉 [View Singleton for Audio Manager](https://github.com/Stefan-101/SpeedBall/blob/dev_Andreea/Assets/AudioManagerScript.cs)
- ***Singleton for User Statistics*** 👉 [View Singleton for Stats Manager](https://github.com/Stefan-101/SpeedBall/blob/Ionut_Andrei/Assets/StatsManager.cs)
  
# Documentarea folosirii toolurilor de AI in timpul dezvoltarii software
---

# Demo offline 
---
Demo-ul offline poate fi găsit accesând link-ul următor: 👉 [View Demo]()

# Echipa
---
Dezvoltarea jocului Speedball a fost posibilă datorită unei echipe dedicate, formată din
- Andrei Ștefan - Inginerul mișcării: a creat sistemele care dau viață și viteză mașinilor din Speedball.
- Bejan Ștefan - Mâna sigură din spatele serverelor și meniurilor, dedicat experienței impecabile și testării atente.
- Lumînăraru Ionuț-Andrei - Regizorul desfășurării jocului: asigură ritmul prin gestionarea scorului, timpului și încheierii rundelor.
- Onisie Andreea - Artistul și arhitectul vizual al jocului: tot ce vezi, auzi și simți în Speedball îi poartă semnătura.
- Stan David-Florin - Motorul din spatele multiplayer-ului: a asigurat conexiuni stabile, servere solide, un meniu și scene care funcționează impecabil.
