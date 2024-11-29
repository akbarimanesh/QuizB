
using Colors.Net;
using ConsoleTables;
using QuizB;
using QuizB.Dto;
using QuizB.Entity;
using QuizB.MyMemory;
using QuizB.Service;
using static Colors.Net.StringStaticMethods;
ServiceTransaction serviceTransaction = new ServiceTransaction();
ServiceCard serviceCard = new ServiceCard();
Card card = new Card();
Transaction transaction = new Transaction();
int option = 0;
int option1 = 0;
while (true)
{
    try
    {
        do
        {

            Console.Clear();
            ColoredConsole.WriteLine($"{White("1:Login ")}");

            ColoredConsole.Write($"{Blue("please Enter your option : ")}");
            option1 = int.Parse(Console.ReadLine());
            switch (option1)
            {
                case 1:
                    Login();
                    break;

                default:
                    break;
            }


        } while (option1 < 3);
    }
    catch (Exception ex)
    {
        ColoredConsole.WriteLine($"{("Select an option.")}");

    }
    Console.ReadKey();
}
void Login()
{

    Console.Clear();
    ColoredConsole.WriteLine($"{Yellow("*************Login*************")}");
    ColoredConsole.WriteLine($"{Yellow("*******************************")}");
    ColoredConsole.Write($"{Blue("Please Enter NumberCard:")}");
    string numberCard = Console.ReadLine();
    ColoredConsole.Write($"{Blue("Please Enter Password :")}");
    string password = Console.ReadLine();

    var result = serviceCard.Login(numberCard, password);

    if (result.IsSuccess)
    {
        ColoredConsole.WriteLine($"{Yellow("******************************")}");
        ColoredConsole.WriteLine($"{Green(result.IsMessage)}");
        
        Console.ReadKey();
        CardMenu();

    }
    else
    {
        ColoredConsole.WriteLine($"{Yellow("******************************")}");
        ColoredConsole.WriteLine($"{Red(result.IsMessage)}");
        Console.ReadKey();
        Login();
        
    }
}
    
    void CardMenu()
    {
        do
        {


            Console.Clear();
            ColoredConsole.WriteLine($"{White("1:Transfer Price ")}");
            ColoredConsole.WriteLine($"{White("2:Get List of Transection Card ")}");
            ColoredConsole.WriteLine($"{White("3:Exit ")}");

            ColoredConsole.WriteLine($"{Yellow("******************************")}");
            ColoredConsole.Write($"{Blue("please Enter your option :")}");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Transfer();
                    break;
                case 2:
                    ShowTransection();
                    break;
                case 3:
                    Exit();
                    break;


                default:
                    break;
            }


        } while (option < 3);
    }

   


void Transfer()
{
    Console.Clear();
    ColoredConsole.WriteLine($"{Yellow("***************Transfer*************")}");
    ColoredConsole.WriteLine($"{Yellow("*****************************************")}");
    ColoredConsole.Write($"{Blue("Please Enter SourceCardNumber:")}");
    string SourceCardNumber = Console.ReadLine();
    ColoredConsole.Write($"{Blue("Please Enter DestinationCardNumber:")}");
    string DestinationCardNumber = Console.ReadLine();
    ColoredConsole.Write($"{Blue("Please Enter Amount:")}");
    float Amount =float.Parse( Console.ReadLine());
   
  
    var result=serviceTransaction.Transfer( SourceCardNumber,DestinationCardNumber, Amount);
    

    if (result.IsSuccess)
    {
        ColoredConsole.WriteLine($"{Yellow("******************************")}");
        ColoredConsole.WriteLine($"{Green(result.IsMessage)}");

        Console.ReadKey();

    }
    else
    {
        ColoredConsole.WriteLine($"{Yellow("******************************")}");
        ColoredConsole.WriteLine($"{Red(result.IsMessage)}");


        Console.ReadKey();
    }
    

    Console.ReadKey();
}
void ShowTransection()
{
    Console.Clear();
    ColoredConsole.WriteLine($"{Yellow("***************List of Transection*************")}");
    ColoredConsole.WriteLine($"{Yellow("*****************************************")}");
    ColoredConsole.Write($"{Blue("Please Enter CardNumber:")}");
    string cardnumber = Console.ReadLine();
    var card1 = serviceTransaction.GetListOfTransactions(cardnumber);
    ConsoleTable.From<GetTrranDto>(card1)
        .Configure(o => o.NumberAlignment = Alignment.Right)
        .Write(Format.Minimal);
    Console.ReadKey();
}
void Exit()
{
    MemoryDb.CurrentCard = null;
    ColoredConsole.WriteLine($"{Red("Logout.")}");
}

