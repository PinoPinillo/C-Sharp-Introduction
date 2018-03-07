using System;
using BlackJackDLL;

namespace BlackJack
{
    class BlackJackGame
    {
        static void Main(string[] args)
        {
            // Controls game loop
            bool restartGame;

            do
            {
                // Initializates some values
                restartGame = false;
                int playersCount = 0;
                Player[] players;

                // Creates the deck and shuffles it
                Deck deck = new Deck();
                deck.ShuffleDeck();

                // Shows a welcome message and ask for the number of players
                Console.WriteLine("Welcome to Black Jack Game! Please, enter the number of players:");
                // The numbers of players in the game must be greater than 0 and less than 5
                while (playersCount <= 0 || playersCount > 4)
                {
                    // Try parsing the line read
                    int.TryParse(Console.ReadLine(), out playersCount);
                    // Shows a warning message
                    if (playersCount <= 0 || playersCount > 4)
                        Console.WriteLine("Please, enter a valid number greater than 0 and less than 5:");
                }

                Console.WriteLine("\n----------\n");

                // Players array length will be the playersCount + the croupier
                players = new Player[playersCount + 1];

                // The croupier will be the first player of the array
                players[0] = new Player("Croupier");

                // Ask for player's name
                for (int i = 1; i < players.Length; i++)
                {
                    Console.WriteLine("Player {0}, enter your name:", i);
                    string playerName = Console.ReadLine();

                    // Player's name can not be croupier or similar. Names by default will be PlayerX
                    if (playerName == "" || playerName.ToLower() == "croupier")
                    {
                        players[i] = new Player("Player" + i);
                        Console.WriteLine("Your name can not be null or Croupier. Your name is {0}\n", players[i].Name);
                    }
                    else
                    {
                        players[i] = new Player(playerName);
                        Console.WriteLine("Your name is {0}\n", playerName);
                    }
                }

                Console.WriteLine("----------\n");

                // Gives each player their starting cards
                foreach (Player player in players)
                {
                    // The croupier's will get 1 card and the players will get 2
                    if (player.Name == "Croupier")
                    {
                        // Gets a card from the deck and adds it to the croupier's cards
                        Card cardToAdd = deck.AskForCard();
                        player.cards.Add(cardToAdd);
                        // Shows the card information and its value
                        Console.WriteLine("Croupier first card is {0}. Croupier total score is {1}",
                            cardToAdd.ToString(),
                            player.GetTotalValue());
                    }
                    else
                    {
                        // Gets 2 cards from the deck and adds them to the player's cards
                        Card card1 = deck.AskForCard();
                        Card card2 = deck.AskForCard();
                        player.cards.Add(card1);
                        player.cards.Add(card2);

                        // Gets the total player value
                        int value = player.GetTotalValue();
                        // Message shown by default
                        string message = string.Format("{0} cards are {1} and {2}. {0} total score is {3}",
                            player.Name,
                            card1.ToString(),
                            card2.ToString(),
                            value);

                        // If the player has BlackJack, adds the string to the default message
                        if (value == 21)
                        {
                            message += "You have BlackJack!";
                            player.BlackJack = true;
                        }

                        // Shows the final message
                        Console.WriteLine(message);
                    }
                }

                Console.WriteLine("\n----------");

                // Players turns
                for (int i = 1; i < players.Length; i++)
                {
                    // If the player has BlackJack continues with the next one
                    if (players[i].BlackJack)
                        continue;

                    // Shows the name of the player who is playing now
                    Console.WriteLine("It is {0} turn!", players[i].Name);
                    Console.WriteLine("----------");

                    // Initializates nextPlayer flag
                    bool nextPlayer = false;
                    // While the nextPlayer value is false, the player can ask for more cards or pass
                    while (!nextPlayer)
                    {
                        // Shows the player's cards
                        Console.WriteLine(players[i].ToString());
                        // Shows the player's losing probability when asking for more cards
                        Console.WriteLine("Your losing probability is {0:0.00}\n",
                            deck.CalculateLosingProbability(players[i].GetTotalValue()));

                        // Ask for player action
                        Console.WriteLine("What do you want to do?");
                        Console.WriteLine("C = more cards || Any other key = pass");

                        // If the player enters a c, asks for one card. Otherwise, passes the turn
                        if (Console.ReadLine().ToLower() == "c")
                        {
                            // Gets the card and adds it to the player's cards
                            Card card = deck.AskForCard();
                            players[i].cards.Add(card);
                            // Shows the card information
                            Console.WriteLine("You asked for more cards and you get {0}!", card.ToString());

                            // If the player has a score of 21 or more, passes the turn
                            if (players[i].GetTotalValue() == 21)
                            {
                                Console.WriteLine("You have a total score of 21, you pass the turn!");
                                Console.WriteLine("----------");
                                nextPlayer = true;
                            }
                            else if (players[i].GetTotalValue() > 21)
                            {
                                Console.WriteLine("Sorry, you exceeded 21. You lose :(");
                                Console.WriteLine("----------");
                                nextPlayer = true;
                            }

                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("{0} passed\n", players[i].Name);
                            Console.WriteLine("----------");
                            nextPlayer = true;
                        }
                    }
                }

                // Shows that it is the croupier's turn
                Console.WriteLine("It is Croupier turn!");
                Console.WriteLine("----------");

                // The croupier must ask for cards if the score is less than or equal to 16
                while (players[0].GetTotalValue() <= 16)
                {
                    Card card = deck.AskForCard();
                    players[0].cards.Add(card);
                    Console.WriteLine("Croupier got {0}! Croupier total score is {1}",
                        card.ToString(), players[0].GetTotalValue());
                }

                Console.WriteLine("\n----------\n");
                Console.WriteLine("Game is over!\n");
                // Shows the croupier's total score
                Console.WriteLine("Croupier total score is {0}", players[0].GetTotalValue());

                // Shows final results
                for (int i = 1; i < players.Length; i++)
                {
                    // Gets the player's score
                    int playerScore = players[i].GetTotalValue();

                    // If the player exceeded 21 points or his score is less than the croupier,
                    // the player has lost the game
                    if (playerScore > 21 || playerScore < players[0].GetTotalValue())
                    {
                        Console.WriteLine("{0} loses with a total score of {1}",
                           players[i].Name, players[i].GetTotalValue());
                    }
                    else
                    {
                        // If the player has a score greater than the croupier, the player has beaten the croupier
                        if (playerScore > players[0].GetTotalValue())
                        {
                            Console.WriteLine("{0} beats the Croupier with a total score of {1}",
                                players[i].Name, players[i].GetTotalValue());
                        }
                        // If the player has a score equal to the croupier, the player ties with the croupier
                        else
                        {
                            Console.WriteLine("{0} ties with the Croupier",
                                players[i].Name);
                        }
                    }
                }

                Console.WriteLine("\n----------\n");
                // Shows how to restart the game
                Console.WriteLine("Press r if you want to play again or any other key to exit");

                // If the player enters a r, restart the game
                if (Console.ReadLine().ToLower() == "r")
                {
                    restartGame = true;
                }

            } while (restartGame);
        }
    }
}
