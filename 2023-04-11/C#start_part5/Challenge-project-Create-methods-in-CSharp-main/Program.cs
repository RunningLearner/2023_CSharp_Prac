using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] playerStates = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = playerStates[0];

// Current player Speed
int speed = 1;

// Index of the current food
int food = 0;

InitializeGame();

while (!shouldExit)
{
    shouldExit = IsTerminalResized();
    if (!shouldExit)
    {
        Move();
    }
}

Console.Clear();
Console.WriteLine("Console was resized. Program exiting.");

// Returns true if the Terminal was resized 
bool IsTerminalResized()
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood()
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer()
{
    player = playerStates[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
void FreezePlayer()
{
    System.Threading.Thread.Sleep(1000);
    player = playerStates[0];
}

//현재 상태를 확인하여 속도를 변화
void CheckState()
{
    if (player == "(^-^)") speed = 3;
    else speed = 1;
}

// Reads directional input from the Console and moves the player
void Move()
{
    int lastX = playerX;
    int lastY = playerY;

    CheckState();

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY -= speed;
            break;
        case ConsoleKey.DownArrow:
            playerY += speed;
            break;
        case ConsoleKey.LeftArrow:
            playerX -= speed;
            break;
        case ConsoleKey.RightArrow:
            playerX += speed;
            break;
        case ConsoleKey.Escape:
            shouldExit = true;
            break;
    }

    if (IsTerminalResized())
    {
        shouldExit = true;
        return;
    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);

    //음식위치에오면 새로운 음식 소환 및 상태변경
    if (IsPlayerOnFood())
    {
        ChangePlayer();
        ShowFood();
        if (player == playerStates[2])
        {
            FreezePlayer();
        }
    }
}

bool IsPlayerOnFood()
{
    return (playerX == foodX && playerY == foodY);
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}