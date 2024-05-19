using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using Basic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Reflection;

namespace ExcelScaleLib
{
    public class Port
    {
        public enum enum_unit_type
        {
            g,
            dwt,
        }

        private MySerialPort mySerialPort = new MySerialPort();
        public bool Init(string PortName , int Baudrate)
        {
            mySerialPort.Init(PortName, Baudrate, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            bool flag_ok = mySerialPort.SerialPortOpen();
            if (flag_ok)
            {
                Console.WriteLine($"{DateTime.Now.ToDateString()} - [ExcelScaleLib] {PortName}({Baudrate}) 初始化成功");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToDateString()} - [ExcelScaleLib] {PortName}({Baudrate}) 初始化失敗");
            }
            return flag_ok;
        }

        public double? get_weight(enum_unit_type enum_Unit_Type)
        {
            if(enum_Unit_Type == enum_unit_type.g)
            {
                Communication.UART_Command_set_unit_to_g(mySerialPort);
            }
            if (enum_Unit_Type == enum_unit_type.dwt)
            {
                Communication.UART_Command_set_unit_to_dwt(mySerialPort);
            }
            return Communication.UART_Command_get_weight(mySerialPort);
        }
        public bool set_sub_current_weight()
        {
            return Communication.UART_Command_set_sub_current_weight(mySerialPort);
        }
        public bool set_to_zero()
        {
            return Communication.UART_Command_set_to_zero(mySerialPort);
        }

    }
    public class Communication
    {
        public static int UART_Delay = 10;
        public static int UART_RetryNum = 3;
        public static bool ConsoleWrite = false;
        public static int UART_TimeOut = 100;

        public enum enum_Command
        {
            get_weight,
            get_stable_weight,
            set_unit_to_g,
            set_unit_to_dwt,
            set_to_zero,
            set_sub_current_weight,
        }

        static public double? UART_Command_get_weight(MySerialPort MySerialPort)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                MyTimerBasic MyTimer_UART_TimeOut = new MyTimerBasic();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                string command = "RW\r\n";
          

                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data error! \n {command}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteString(command);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        string result = MySerialPort.ReadString();
                  
                        if (result.StringIsEmpty() == false)
                        {
                            result = result.Replace("\0", "");
                            result = result.Replace("\n", "");
                            result = result.Replace("\r", "");
                            double? temp = ExtractNumber(result);
                            if(temp == null)
                            {
                                retry++;
                                cnt = 0;
                                continue;
                            }
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data sucessed! [{temp}] , {myTimerBasic}\n");
                            flag_OK = true;
                            return temp;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(0);
                }
            }
            System.Threading.Thread.Sleep(UART_Delay);
            return null;
        }
        static public bool UART_Command_set_sub_current_weight(MySerialPort MySerialPort)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                MyTimerBasic MyTimer_UART_TimeOut = new MyTimerBasic();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                string command = "MT\r\n";


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data error! \n {command}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteString(command);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(2000);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        string result = MySerialPort.ReadString();

                        if (result.StringIsEmpty() == false)
                        {
                            result = result.Replace("\0", "");
                            result = result.Replace("\n", "");
                            result = result.Replace("\r", "");

                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data sucessed! [{result}] , {myTimerBasic}\n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(0);
                }
            }
            System.Threading.Thread.Sleep(UART_Delay);
            return flag_OK;
        }
        static public bool UART_Command_set_to_zero(MySerialPort MySerialPort)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                MyTimerBasic MyTimer_UART_TimeOut = new MyTimerBasic();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                string command = "MZ\r\n";


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data error! \n {command}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteString(command);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(2000);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        string result = MySerialPort.ReadString();

                        if (result.StringIsEmpty() == false)
                        {
                            result = result.Replace("\0", "");
                            result = result.Replace("\n", "");
                            result = result.Replace("\r", "");

                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data sucessed! [{result}] , {myTimerBasic}\n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(0);
                }
            }
            System.Threading.Thread.Sleep(UART_Delay);
            return flag_OK;
        }
        static public bool UART_Command_set_unit_to_g(MySerialPort MySerialPort)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                MyTimerBasic MyTimer_UART_TimeOut = new MyTimerBasic();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                string command = "UA\r\n";


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data error! \n {command}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteString(command);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        string result = MySerialPort.ReadString();

                        if (result.StringIsEmpty() == false)
                        {
                            result = result.Replace("\0", "");
                            result = result.Replace("\n", "");
                            result = result.Replace("\r", "");

                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data sucessed! [{result}] , {myTimerBasic}\n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(0);
                }
            }
            System.Threading.Thread.Sleep(UART_Delay);
            return flag_OK;
        }
        static public bool UART_Command_set_unit_to_dwt(MySerialPort MySerialPort)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                MyTimerBasic MyTimer_UART_TimeOut = new MyTimerBasic();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                string command = "UH\r\n";


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data error! \n {command}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteString(command);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        string result = MySerialPort.ReadString();

                        if (result.StringIsEmpty() == false)
                        {
                            result = result.Replace("\0", "");
                            result = result.Replace("\n", "");
                            result = result.Replace("\r", "");

                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data sucessed! [{result}] , {myTimerBasic}\n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(0);
                }
            }
            System.Threading.Thread.Sleep(UART_Delay);
            return flag_OK;
        }


        static double? ExtractNumber(string input)
        {
            string pattern = @"[+-]?\s*\d+\.\d+"; // 正則表達式模式
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, pattern);

            if (match.Success)
            {
                // 去除多餘的空格
                string value = match.Value.Replace(" ", "");
                if (double.TryParse(value, out double result))
                {
                    return result;
                }
            }

            return null; // 如果沒有匹配到或者轉換失敗則返回null
        }
    }
}
