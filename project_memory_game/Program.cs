using memory_game;
Console.WriteLine("Enter kind of game: 1 for letter card,2 for symbol card 3 for ExcersizeCard");
Game myGame = new Game((KindOfCards)int.Parse(Console.ReadLine()));


Console.WriteLine("If you want to play with the computer enter 1, else enter 0");
int flag = int.Parse(Console.ReadLine());
int level=0;

if (flag == 0)
{
    Console.WriteLine("Enter number of player");
    myGame.AmountOfPlayer = int.Parse(Console.ReadLine());
    for (int i = 0; i < myGame.AmountOfPlayer; i++)
    {
        Console.WriteLine("player number {0}, Enter your name!", i + 1);
        UserPlayer player = new UserPlayer();
        player.ResetName(Console.ReadLine());
        myGame.listOfPlayer.Add(player);
    }
}
else
{
    myGame.AmountOfPlayer = 2;
    Console.WriteLine("Enter your name!");
    UserPlayer player = new UserPlayer();
    player.ResetName(Console.ReadLine());
    ComputerPlayer computer = new ComputerPlayer();
    myGame.PlaywithThecomputer(computer);
    myGame.listOfPlayer.Add(player);
    myGame.listOfPlayer.Add(computer);
    Console.WriteLine("Enter a level to play with the computer, 1: hard,2: easy");
    level = int.Parse(Console.ReadLine());
}
myGame.RestartGame(flag);
while (myGame.BoardGame.CheckContainsCards())
{
    int card1, card2;
    for (int i = 0; i < myGame.AmountOfPlayer; i++)
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Hi " + myGame.listOfPlayer[i].Name + " pick a card");
        card1 = int.Parse(Console.ReadLine()) - 1;
        while (!myGame.BoardGame.CheckLigalPlace(card1))
        {
            Console.WriteLine("The card is not vallid, Try again");
            card1 = int.Parse(Console.ReadLine()) - 1;
        }
        myGame.Playing(card1);
        Console.WriteLine("pick a second card");
        card2 = int.Parse(Console.ReadLine()) - 1;
        while (!myGame.BoardGame.CheckLigalPlace(card2))
        {
            Console.WriteLine("The card is not vallid, Try again");
            card2 = int.Parse(Console.ReadLine()) - 1;
        }
        myGame.Playing(card2);
        myGame.FinishToPick(card1, card2, i, flag);
        if (flag == 1)
        {
            card1 = card2 = 0;
            myGame.listOfPlayer[1].ChoiseCard(ref card1, ref card2,level);
            while (!myGame.BoardGame.CheckLigalPlace(card1) || !myGame.BoardGame.CheckLigalPlace(card2))
            {
                card1 = card2 = 0;

                myGame.listOfPlayer[1].ChoiseCard(ref card1, ref card2, level);
            }
            myGame.Playing(card1);
            Thread.Sleep(1000);
            myGame.Playing(card2);
            myGame.FinishToPick(card1, card2, i, flag);
            break;
        }
    }
}
myGame.FinishGame();


