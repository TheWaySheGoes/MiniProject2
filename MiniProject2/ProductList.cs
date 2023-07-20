using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject2
{
    /*
     * Execution starts in main loop and switches between: Searching products,
     * adding products, and showing the products table. There are safe guards for not int for price and multiple
     * outs from loops. 
     * 
     */
    internal class ProductList
    {
        List<Product> products = new List<Product>();
        String exitOrProductMsg = "To enter a new product - follow the steps | To quit - enter: 'Q'";
        String exitOrProductOrSearchMsg = "To enter a new product enter 'P' | To search for product enter 'S' | To quit - enter: 'Q'";
        String categoryMsg = "Enter Category: ";
        String productMsg = "Enter Product: ";
        String priceMsg = "Enter Price: ";
        String priceWholeNumberMsg = "Price must be a whole number ex. 1234";
        String tableHeader = "------------------------------------------------------------"; 
        String tableCategories = "Category".PadLeft(20) + "Product".PadLeft(20) + "Price".PadLeft(20);
        String tableFooter = "------------------------------------------------------------";
        bool exitProductLoop = false;
        bool exitMainLoop = false;
        bool exitSearchLoop = false;
        LoopType loopType = LoopType.PRODUCTS;
        String productsKeyWord = "P";
        String quitKeyWord = "Q";
        String searchKeyWord = "S";

        /*
         * Main logic, throwing the execution to specific loop
         */
        public void mainLoop()
        {
            while (!exitMainLoop)
            {
                switch (loopType)
                {
                    case LoopType.SEARCH:
                        {
                            searchLoop();
                            break;
                        }
                    case LoopType.PRODUCTS:
                        {
                            productLoop();
                            break;
                        }
                    case LoopType.SHOW_PRODUCT_TABLE:
                        {
                            showProductTable();
                            break;
                        }
                }
            }
        }

        /*
         * shows the product table highlighting the searched product
         */
        private void searchLoop()
        {
            while (!exitSearchLoop)
            {
                Console.Write(productMsg);
                String input = Console.ReadLine();

                printMsg(tableHeader);
                printMsg(tableCategories, Color.GREEN);
                products.OrderBy(product => product.Price).ToList().ForEach(product =>
                {
                    if (product.ProductName == input)
                    {
                        printMsg(product.toString(), Color.CYAN);
                    }
                    else
                    {
                        printMsg(product.toString());
                    }
                });
                printMsg(tableFooter);
                printMsg(exitOrProductOrSearchMsg, Color.BLUE);
                input = Console.ReadLine();

                if (input == productsKeyWord)
                {
                    loopType = LoopType.PRODUCTS;
                    exitSearchLoop = true;
                }
                else if (input == searchKeyWord)
                {
                    loopType = LoopType.SEARCH;
                    exitSearchLoop = false;
                }
                else if (input == quitKeyWord)
                {
                    exitSearchLoop = true;
                    exitMainLoop = true;
                }
                //TODO exit
                else { }
            }
            exitSearchLoop = false;
        }

        /*
         * Adds a product to the list, exits and shows products table otherwise
         */
        private void productLoop()
        {
            while (!exitProductLoop)
            {
                String category = null;
                String product = null;
                int price = 0;

                //add category or exit
                printMsg(exitOrProductMsg, Color.YELLOW);
                Console.Write(categoryMsg);
                String input = Console.ReadLine().Trim();
                if (isExitProductsLoop(input, quitKeyWord))
                {
                    exitProductLoop = true;
                }
                else if (!exitProductLoop)
                {
                    category = input;
                }

                //add product name or exit
                if (!exitProductLoop)
                {
                    Console.Write(productMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        product = input;

                    }
                }
                
                //add price (int) or exit
                if (!exitProductLoop){
                    Console.Write(priceMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        while (!int.TryParse(input, out int number))
                        {
                            printMsg(priceWholeNumberMsg, Color.RED);
                            input = Console.ReadLine().Trim();
                            if (isExitProductsLoop(input, quitKeyWord))
                            {
                                exitProductLoop = true;
                                break;
                            }
                        }
                        if (!exitProductLoop)
                        {
                            price = int.Parse(input);
                        }
                    }
                }

                if (!exitProductLoop)
                {
                    Product tempProduct = new Product();
                    tempProduct.Category = category;
                    tempProduct.ProductName = product;
                    tempProduct.Price = price;
                    products.Add(tempProduct);
                }

            }
            exitProductLoop = false;
            loopType = LoopType.SHOW_PRODUCT_TABLE;
        }

        /*
         * Generates a table with all of the products in the product list, sorted by price, with sum 
         * 
         */
        private void showProductTable()
        {
            printMsg(tableHeader);
            printMsg(tableCategories, Color.GREEN);
            products.OrderBy(product => product.Price).ToList().ForEach(product => printMsg(product.toString()));
            printMsg(tableFooter);
            Console.WriteLine(products.Sum(product => product.Price).ToString().PadLeft(60));
            printMsg(exitOrProductOrSearchMsg, Color.BLUE);
            String input = Console.ReadLine();
            
            if (input == productsKeyWord)
            {
                loopType = LoopType.PRODUCTS;
            }else if (input == searchKeyWord)
            {
                loopType = LoopType.SEARCH;
            }else if (input == quitKeyWord)
            {
                exitMainLoop = true;
            }
            //TODO exit
            else { }
        }

        /*
         * Checks if exitCommand is typed in console input
         */
        private bool isExitProductsLoop(String msg, String exitCommand)
        {
            if (msg == exitCommand)
            {
                return true;
            }
            return false;
        }

        /*
         * Changes console output foreground color to one of pre defined Enums. 
         * After that goes back to default white.
         * 
         */
        private void printMsg(String msg, Color color = Color.WHITE)
        {
            switch (color)
            {
                case Color.WHITE:
                    Console.WriteLine(msg);
                    break;
                case Color.GREEN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    break;
                case Color.BLUE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(msg);
                    break;
                case Color.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(msg);
                    break;
                case Color.CYAN:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(msg);
                    break;
                case Color.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    break;
            }

            if (Console.ForegroundColor != ConsoleColor.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


    }
}

    enum Color
    {
        WHITE,
        BLUE,
        GREEN,
        YELLOW,
        CYAN,
        RED
    }

    enum LoopType
    {
        SEARCH,
        PRODUCTS,
        SHOW_PRODUCT_TABLE
    }

