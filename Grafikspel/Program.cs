using System;
using System.Numerics;
using Raylib_cs;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(1920, 1080, "Hello");
        // Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);

        int player1x = 100;
        int player1y = 100;
        int player1width = 50;
        int player1height = 50;
        int goalexpandx = 50;
        int goalexpandy = 50;
        int enemyx = 1400;
        int enemyy = 200;
        int point1xpos = 200;
        int point1ypos = 400;
        int point2xpos = 400;
        int point2ypos = 400;
        int point3xpos = 600;
        int point3ypos = 400;
        int point1width = 25;
        int point1height = 25;
        int point2width = 25;
        int point2height = 25;
        int point3width = 25;
        int point3height = 25;

        int rows = 5;
        int cols = 5;

        bool scenestart = true;
        bool scenegame1 = false;
        bool scenegame2 = false;
        string starttext = "Press SPACE to start!";
        int pointamount = 0;
        int levelamount = 1;
        string pointcount = "Points: ";
        string currentlevel = "Level ";
        float enemyradius = 50;

        float playerradius = 50;

        float levelradius2 = 2500;

        int[,] grid = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,1,0},
            {1,0,0,0,0,0,0,0,0,0,0,1,0},
            {1,0,0,0,0,0,0,1,1,0,0,1,0},
            {1,0,0,0,0,0,0,1,1,0,0,1,0},
            {1,0,0,0,0,0,0,0,0,0,0,1,0},
            {1,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        int tileSize = 32;

        List<Rectangle> walls = new();

        for (int x = 0; x < grid.GetLength(1); x++)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                // System.Console.WriteLine($"x: {x}, y: {y}");
                if (grid[y, x] == 1)
                {
                    Rectangle wall = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    walls.Add(wall);
                }
            }
        }



        while (!Raylib.WindowShouldClose())
        {


            while (scenestart)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);
                Raylib.DrawText(starttext, 600, 500, 60, Color.BLACK);
                Raylib.EndDrawing();

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    scenestart = false;
                    scenegame1 = true;

                }
            }

            while (scenegame1)
            {
                Vector2 enemycenter = new Vector2(enemyx, enemyy);
                Vector2 playercenter = new Vector2(player1x, player1y);
                Vector2 point1center = new Vector2(point1xpos, point1ypos);
                Vector2 point2center = new Vector2(point2xpos, point2ypos);
                Vector2 point3center = new Vector2(point3xpos, point3ypos);


                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);

                foreach (Rectangle wall in walls)
                {
                    Raylib.DrawRectangleRec(wall, Color.PINK);
                    Raylib.DrawRectangleLinesEx(wall, 1, Color.YELLOW);
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    player1x += 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    player1x -= 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    player1y += 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    player1y -= 8;
                }

                //Rectangle playerRect = new Rectangle(player1x, player1y, player1width, player1height);
                //Rectangle enemyRect = new Rectangle(enemyx, enemyy, goalexpandx, goalexpandy);
                Rectangle point1 = new Rectangle(point1xpos, point1ypos, point1width, point1height);
                Rectangle point2 = new Rectangle(point2xpos, point2ypos, point2width, point2height);
                Rectangle point3 = new Rectangle(point3xpos, point3ypos, point3width, point3height);





                bool areOverlapping1 = Raylib.CheckCollisionCircles(playercenter, playerradius, enemycenter, enemyradius);
                bool areOverlapping2 = Raylib.CheckCollisionCircleRec(playercenter, playerradius, point1);
                bool areOverlapping3 = Raylib.CheckCollisionCircleRec(playercenter, playerradius, point2);
                bool areOverlapping4 = Raylib.CheckCollisionCircleRec(playercenter, playerradius, point3);


                Raylib.DrawCircleV(playercenter, playerradius, Color.BLACK);


                Texture2D Monkey = Raylib.LoadTexture("horunge.png");



                if (areOverlapping1 == true)
                {

                    enemyradius += 50;


                    pointamount = 0;

                    if (enemyradius > 2500)
                    {
                        scenegame1 = false;
                        scenegame2 = true;

                    }
                    if (levelamount < 2)
                    {
                        levelamount += 1;
                    }

                }
                if (areOverlapping2 == true)
                {
                    point1xpos = -2000;
                    pointamount += 1;
                }
                if (areOverlapping3 == true)
                {
                    point2xpos = -2100;
                    pointamount += 1;
                }
                if (areOverlapping4 == true)
                {
                    point3xpos = 2200;
                    pointamount += 1;
                }




                Raylib.DrawRectangleRec(point1, Color.GREEN);
                Raylib.DrawRectangleRec(point2, Color.GREEN);
                Raylib.DrawRectangleRec(point3, Color.GREEN);
                //  Raylib.DrawRectangleRec(enemyRect, Color.BLUE);
                // Raylib.DrawRectangleRec(playerRect, Color.BLACK);


                if (player1x > 1870)
                {
                    player1x -= 8;
                }
                if (player1x < 50)
                {
                    player1x += 8;
                }
                if (player1y > 1030)
                {
                    player1y -= 8;
                }
                if (player1y < 50)
                {
                    player1y += 8;
                }
                Raylib.DrawText(pointcount + pointamount + "/3", 50, 50, 40, Color.WHITE);
                Raylib.DrawText(currentlevel + levelamount, 900, 50, 40, Color.WHITE);
                Raylib.DrawCircleV(enemycenter, enemyradius, Color.RED);
                Raylib.EndDrawing();



            }

            while (scenegame2)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGRAY);
                Raylib.DrawCircle(player1x, player1y, levelradius2, Color.RED);
                if (scenegame2 == true)
                {
                    levelradius2 -= 50;
                }
                Vector2 enemycenter = new Vector2(enemyx, enemyy);
                Vector2 playercenter = new Vector2(player1x, player1y);


                //Rectangle playerRect = new Rectangle(player1x, player1y, player1width, player1height);

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    player1x += 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    player1x -= 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    player1y += 8;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    player1y -= 8;
                }

                if (player1x > 1810)
                {
                    player1x -= 8;
                }
                if (player1x < 5)
                {
                    player1x += 8;
                }
                if (player1y > 965)
                {
                    player1y -= 8;
                }
                if (player1y < 10)
                {
                    player1y += 8;
                }





                // Raylib.DrawRectangleRec(playerRect, Color.BLACK);
                Raylib.DrawCircleV(playercenter, playerradius, Color.BLACK);
                Raylib.DrawText(pointcount + pointamount + "/3", 50, 50, 40, Color.WHITE);
                Raylib.DrawText(currentlevel + levelamount, 900, 50, 40, Color.WHITE);
                Raylib.EndDrawing();

            }
        }
    }
}