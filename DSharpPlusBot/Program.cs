using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlusBot
{
	class Program
	{
		static void Main(string[] args)
		{
            var bot = new Bot();

            Task.Run(async () =>
            {
                await bot.RunAsync();
                while (true)
                {
                    await Task.Delay(500);
                }
            }).Wait();

            // TODO nice shutdown logic
        }
	}
}
