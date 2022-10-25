using System;
using System.Collections.Generic;

namespace CyberForum
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.StartGame();
        }
    }

    public class Game
    {
        public List<Card> _Koloda;
        public List<Player> _Рlayers;

        Random _random;
        int _cardsAmount = 36;
        int count = 2;
        public Game()
        {
            do
            {
                Console.WriteLine("Введите ко-во игроков");
                count = int.Parse(Console.ReadLine());
                if (count != 5 && count != 7 && count > 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }

            } while (true);
            _random = new Random();

            _Рlayers = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                _Рlayers.Add(new Player());
            }

            _Koloda = createKoloda();
            RandKoloda(_Koloda);
            Rasdacha(_Рlayers, _Koloda);
        }

        public List<Card> createKoloda()
        {
            _Koloda = new List<Card>();
            int typecardcount = _cardsAmount / 4;

            for (int i = 0; i < typecardcount; i++)
            {
                _Koloda.Add(new Card((CardValue)i, (CardSuit)0));
                _Koloda.Add(new Card((CardValue)i, (CardSuit)1));
                _Koloda.Add(new Card((CardValue)i, (CardSuit)2));
                _Koloda.Add(new Card((CardValue)i, (CardSuit)3));
            }

            return _Koloda;
        }

        public void RandKoloda(List<Card> cards)
        {
            cards.Sort((a, b) => _random.Next(0, 2));
        }
        public void Rasdacha(List<Player> players, List<Card> cards)
        {
            int currentPlayer = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                players[currentPlayer].cards.Add(cards[i]);
                currentPlayer++;
                currentPlayer %= players.Count;
            }
        }
        public void StartGame()
        {
            Console.WriteLine("  Игра началась ");
            do
            {
                int maxValue = -1;
                Player playerWithMaxValue = null;
                Stack<Card> cardStack = new Stack<Card>();

                for (int i = 0; i < _Рlayers.Count; i++)
                {
                    Player player = _Рlayers[i];

                    if (player.cards.Count > 0)
                    {
                        Card card = player.cards[_random.Next(player.cards.Count)];

                        Console.WriteLine($"Игрок {i+1}  Положил карту {card}");
                        player.cards.Remove(card);

                        if ((int)card._Value > maxValue)
                        {
                            maxValue = (int)card._Value;
                            playerWithMaxValue = player;
                        }

                        cardStack.Push(card);

                    }
                }

                playerWithMaxValue.cards.AddRange(cardStack);
                Thread.Sleep(700);
                Console.WriteLine($"Карты забрал игрок {_Рlayers.IndexOf(playerWithMaxValue)+1}\n");

                if (playerWithMaxValue.cards.Count == _cardsAmount)
                {
                    Console.WriteLine($"\nПобедил игрок номер {_Рlayers.IndexOf(playerWithMaxValue)+1} !!");
                    break;
                }

            } while (true);
        }
    }

    public class Player
    {
        public List<Card> cards = new List<Card>();
    }

    public enum CardValue
    {
        Шесть = 0, Семь, Восемь, Девять, Десять, Валет, Дама, Король, Туз
    }

    public enum CardSuit
    {
        Черви = 0, Буби, Трефы, Пики
    }
    public class Card
    {

        public readonly CardValue _Value;
        public readonly CardSuit _Suit;

        public Card(CardValue value, CardSuit suit)
        {
            _Value = value;
            _Suit = suit;
        }

        public override string ToString()
        {
            return $"{_Value} {_Suit}";
        }
    }
}