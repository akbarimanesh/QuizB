
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
ServiceUser serviceUser = new ServiceUser();
Card card = new Card();
User user = new User();
Transaction transaction = new Transaction();
int option = 0;
int option1 = 0;
//while (true)
//{
//    try
//    {
//        do
//        {

//            Console.Clear();
//            ColoredConsole.WriteLine($"{White("1:Login ")}");

//            ColoredConsole.Write($"{Blue("please Enter your option : ")}");
//            option1 = int.Parse(Console.ReadLine());
//            switch (option1)
//            {
//                case 1:
//                    Login();
//                    break;

//                default:
//                    break;
//            }


//        } while (option1 < 3);
//}
//    catch (Exception ex)
//    {

//        ColoredConsole.Write($"{Red("Select an option.")}");

//    }
//    Console.ReadKey();
//}
Login();
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
            ColoredConsole.WriteLine($"{White("3:Card Balance Display ")}");
            ColoredConsole.WriteLine($"{White("4:Change Card Password ")}");
            ColoredConsole.WriteLine($"{White("5:Exit ")}");

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
                    BalanceDisplay();
                    break;
                case 4:
                    ChangePassword();
                    break;
                case 5:
                    Exit();
                    break;


                default:
                    break;
            }


        } while (option < 5);
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
    var hoderName= serviceCard.DisplayHolderName(DestinationCardNumber);
    ColoredConsole.WriteLine();
    ColoredConsole.WriteLine($"{Yellow("HoderName Card is :")}{hoderName}");
    ColoredConsole.Write($"{Yellow("Do you confirm? y/n : ")}");
    string answer = Console.ReadLine();
    ColoredConsole.WriteLine();
    if (answer=="y")
    {
        ColoredConsole.Write($"{Blue("Please Enter Amount:")}");
        float Amount = float.Parse(Console.ReadLine());
        serviceTransaction.GenerateVerificationCode(SourceCardNumber);
        ColoredConsole.Write($"{Blue("Enter the code sent:")}");
        string code = Console.ReadLine();
       if(serviceTransaction.IsVerificationCode(SourceCardNumber, code))
        {
            var result = serviceTransaction.Transfer(SourceCardNumber, DestinationCardNumber, Amount);


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
        }
        else ColoredConsole.Write($"{Red("Your code has expired.")}");
    }
    else if (answer == "n")
        ColoredConsole.WriteLine($"{Red("Deposit canceled.")}");


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
void BalanceDisplay()
{
    try
    {
        Console.Clear();
        ColoredConsole.WriteLine($"{Yellow("***************Card Balance Display*************")}");
        ColoredConsole.WriteLine($"{Yellow("*****************************************")}");
        ColoredConsole.Write($"{Blue("Please Enter CardNumber:")}");
        string cardnumber = Console.ReadLine();
        var card = serviceUser.BalanceDisplay(cardnumber);
        ColoredConsole.Write($"{Green("Your card balance is: ")}{card.Balance}$");
    }
    catch (Exception ex)
    {

        ColoredConsole.Write($"{Red(ex.Message)}");

    }

    Console.ReadKey();
}
void ChangePassword()
{
    try
    {
        Console.Clear();
        ColoredConsole.WriteLine($"{Yellow("***************Change Card Password*************")}");
        ColoredConsole.WriteLine($"{Yellow("*****************************************")}");
        ColoredConsole.Write($"{Blue("Please Enter CardNumber:")}");
        string cardnumber = Console.ReadLine();
        ColoredConsole.Write($"{Blue("Please Enter OldPassword:")}");
        string oldPassword = Console.ReadLine();
        ColoredConsole.Write($"{Blue("Please Enter NewPassword:")}");
        string newPassword = Console.ReadLine();
         var result=serviceUser.ChangeCardPassword(cardnumber,oldPassword,newPassword);
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
    }
    catch (Exception ex)
    {

        ColoredConsole.Write($"{Red(ex.Message)}");

    }

    Console.ReadKey();
}
void Exit()
{
    MemoryDb.CurrentCard = null;
    ColoredConsole.WriteLine($"{Red("Logout.")}");
    Console.ReadKey();
    Console.Clear();
    Login();
}

