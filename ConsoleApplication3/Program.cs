using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            var vk = new VkNet.VkApi();
            Console.WriteLine("Привет!");
            Console.WriteLine("Вам необходимо ввести Access Token\nВы можете сделать это вручную, либо создать файл at.txt на диске C");
            label_1:
            Console.WriteLine("\nЕсли вы хотите ввести код вручную нажмите 1\nЕсли у вас имеется файл C:\\at.txt нажмите 2\nЕсли вы хотите вручную ввести расположения файла at.txt нажмите 3");
            var pw = Console.ReadKey();
           
            if ((pw.Key.ToString() == "D1") || (pw.Key.ToString() == "NumPad1"))
            {
                Console.CursorLeft = Console.CursorLeft - 1;
                Console.Write("Введите access_token: ");
                vk.AccessToken = Console.ReadLine();
            }
            else if (((pw.Key.ToString() == "D2") || (pw.Key.ToString() == "NumPad2")))
            {
                string line;
                using (StreamReader reader = new StreamReader("C:\\at.txt"))
                {
                    line = reader.ReadLine();
                }
                Console.CursorLeft = Console.CursorLeft - 1;
                Console.WriteLine(String.Format("Access Token: {0}",line));
                vk.AccessToken = line;
            }
            else if (((pw.Key.ToString() == "D3") || (pw.Key.ToString() == "NumPad3")))
            {
                 label_2:
                try
                {
                    Console.CursorLeft = Console.CursorLeft - 1;
                }
                catch
                {

                }
                Console.WriteLine("Введите расположения папки, в оторой находится файл at.txt, содержащий Access Token.\n Пример: C:\\");
                string path = Console.ReadLine();
            
                if (path[(path.Length)-1]=='\\'){
                    path += "at.txt";
                }
                else { path += "\\at.txt"; }
    
                string line;
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        line = reader.ReadLine();
                    }
                }
                catch
                {
                    Console.WriteLine("Возникла ошибка, попробуйте еще раз.");
                    goto label_2;
                }
              
                Console.WriteLine(String.Format("Access Token: {0}", line));
                vk.AccessToken = line;
            }
            else
            {
                Console.CursorLeft = Console.CursorLeft - 1;
                Console.WriteLine("Ошибка в выборе решения!");
                goto label_1;
            }
     

           
            vk.Authorize(vk.AccessToken);
            var info  = vk.Messages.GetLongPollServer();
            Console.WriteLine("TS: " + info.Ts.ToString() + "\nKey: " + info.Key.ToString() + "\nServer: " + info.Server.ToString());
            string pattern = @"\[4,[0-9]*";
            string pattern2 = @"[0-9][0-9]+";
           Regex dr = new Regex(pattern);
           Regex dr2 = new Regex(pattern2);
           Console.WriteLine("\nРабота программы началась, абисрака затрален!\n");
            for (; ; )
            {
                
                var p = VkNet.Categories.LongPoll.Run(info);
                p = p.Replace(Environment.NewLine, "");
                p = p.Replace(" ", "");
                
         var w = dr.Matches(p);
  

                foreach (var el in w)
                {
                 string id = dr2.Match(el.ToString()).ToString();
                 var mes = vk.Messages.GetById(long.Parse(id));
                 if (mes.UserId == ((1488 * 228) + (2 * 3 * 17 * 387911)))
                 {
                     vk.Messages.Delete(long.Parse(id));
                     Console.WriteLine("Сообщение Сани удалено: " + mes.Body);
                 }
                }
        
              
            
            }
            Console.ReadLine();
        }
        




    
        asd
    }
}
