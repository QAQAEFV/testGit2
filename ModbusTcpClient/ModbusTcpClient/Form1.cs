using Modbus.Device;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ModbusTcpClient
{
    public partial class Form1 : Form
    {
        int TPI = 0;//用于事务处理标识符递增用

        bool isChoose = false;//单选框1是否被选中
        bool isChoose2 = false;//单选框2是否被选中

        bool buttonState = true;


        ManualResetEvent resetEvent = new ManualResetEvent(true);
        //控制线程的暂停和开始
        bool flag1 = false;
        Task task = null;//新线程
        int count = 0;//表格滚动控制按钮所需数据

        //被点击表格的横纵坐标
        public static int x;
        public static int y;
        Socket socket;
        public static string tempStr;//用来存放通过表格修改寄存器内容的时候需要的窗体之间数据的传送
        public static string RowNumber;//用来存放点击表格的地址


        delegate void AddSendInfoDel(string sendStr);

        AddSendInfoDel sendDel;

        public Form1()
        {
            InitializeComponent();
            sendDel += SendMsg;
        }


        #region 建立连接&断开连接按钮点击事件
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {

            if (buttonState)
            {

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(IpAddressTextBox.Text.Trim());
                IPEndPoint ipe = new IPEndPoint(address, int.Parse(PortTextBox.Text.Trim()));
                try
                {

                    socket.Connect(ipe);

                    if (socket.Connected)
                    {
                        MessageBox.Show("已连接服务端");
                    }
                    buttonState = false;

                    task = new Task(() =>
                    {
                        ListenRadioButtun();

                    });
                    task.Start();


                }
                catch
                {
                    MessageBox.Show("连接失败，请检查ip地址以及端口号是否填写正确\r\n  \t或者服务端是否正常开启");
                }
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (!buttonState)
            {
                //禁用收发，确保close前，已全部接收或发送
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                if (!socket.Connected)
                {
                    MessageBox.Show("已断开服务端");
                    buttonState = true;
                }
            }
         
        }
        #endregion

        #region 线程函数

        private void ListenRadioButtun()
        {

            while (!buttonState)
            {
                if (flag1 == false)//看展示按钮有没有在运行，如果展示按钮在运行那么我就不展示
                {
                    if (radioButton2.Checked)
                    {
                        ReadAllCoil();
                    }
                    else if (radioButton1.Checked)
                    {
                        ReadAllRegister();

                    }
                }

            }
        }

        private void ListenShowDataButtun()
        {
            if (Convert.ToUInt16(CountTextBox.Text) > 0 && Convert.ToUInt16(CountTextBox.Text) <= 65535)
            {
                while (flag1)
                {
                    if (FunctionTextBox.Text == "1")
                    {
                        ReadWantCountCoil();

                    }
                    else if (FunctionTextBox.Text == "3")
                    {
                        ReadWantCountRegister();
                    }

                }
            }
            else
            {
                MessageBox.Show("读取字寄存器数量在0到65535");
            }
        }
        #endregion

        #region 单选框点击事件

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (isChoose)
            {
                radioButton1.Checked = false;
                isChoose = false;
            }
            else
            {
                radioButton1.Checked = true;
                isChoose = true;

            }
        }



        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (isChoose2)
            {
                radioButton2.Checked = false;
                isChoose2 = false;
            }
            else
            {
                radioButton2.Checked = true;
                isChoose2 = true;
            }
        }

        #endregion

        #region 读取数据按钮点击事件

        private void ShowAllData_Click(object sender, EventArgs e)
        {
            if(Convert.ToUInt16(CountTextBox.Text) + Convert.ToUInt16(StartAddressTextBox.Text) > 65535)
            {
                MessageBox.Show("读取超出范围");
            }
            else
            {
                if (!buttonState)
                {
                    if (FunctionTextBox.Text != "" && StartAddressTextBox.Text != "" && CountTextBox.Text != "")
                    {
                        if (FunctionTextBox.Text == "1" || FunctionTextBox.Text == "3")
                        {
                            if (flag1 == false)
                            {
                                flag1 = true;
                                ShowAllData.Text = "Showing";

                            }
                            else if (flag1 == true)
                            {
                                flag1 = false;
                                ShowAllData.Text = "ShowData";
                                count = 0;
                                List<dataObject> datalist = new List<dataObject>();
                                dataShowArea.DataSource = datalist;
                            }
                            task = new Task(() =>
                            {
                                ListenShowDataButtun();

                            });
                            task.Start();
                        }
                        else
                        {
                            MessageBox.Show("请输入读取功能码！");
                        }

                    }
                    else
                    {
                        MessageBox.Show("请输入完整信息！");
                    }

                }
                else
                {
                    MessageBox.Show("请先建立连接！");
                }
            }
           
        }

        #endregion

        #region 写入按钮点击事件

        private void Writebutton_Click(object sender, EventArgs e)
        {
            if (!buttonState)
            {
                if (FunctionTextBox.Text != "" && StartAddressTextBox.Text != "" && CountTextBox.Text != "")
                {
                    switch (Convert.ToInt16(FunctionTextBox.Text))
                    {

                        case 5:
                            WriteSingleCoil(FunctionTextBox.Text, StartAddressTextBox.Text, CountTextBox.Text);
                            break;
                        case 6:
                            WriteRegister(FunctionTextBox.Text, StartAddressTextBox.Text, CountTextBox.Text);
                            break;
                        default:
                            MessageBox.Show("请检查功能码输入是否正确");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("请输入完整信息！");
                }

            }
            else
            {
                MessageBox.Show("请先建立连接！");
            }

        }

        #endregion

        #region 控制表格滚动按钮点击事件

        private void NextRow_Click(object sender, EventArgs e)
        {
            if ((count + 10) <= dataShowArea.RowCount - 1)
            {
                count = count + 10;
            }
        }

        private void UpRow_Click(object sender, EventArgs e)
        {
            if (count >= 10)
            {
                count = count - 10;
            }

        }

        #endregion

        #region 表格数据点击改变数据事件

        private void DataGridViewListCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*                resetEvent.Reset();*/
            int CIndex = e.ColumnIndex;
            int RIndex = e.RowIndex;
            x = RIndex;
            y = CIndex;
            RowNumber = dataShowArea.Rows[RIndex].Cells[0].Value.ToString();

            if (CIndex == 2)//点击的是数值一列
            {
                if (dataShowArea.Rows[RIndex].Cells[CIndex].Value.ToString() == "on" || dataShowArea.Rows[RIndex].Cells[CIndex].Value.ToString() == "off")//线圈
                {
                    if (dataShowArea.Rows[RIndex].Cells[CIndex].Value.ToString() == "on")
                    {
                        dataShowArea.Rows[RIndex].Cells[CIndex].Value = "off";
                        WriteSingleCoil("5", dataShowArea.Rows[RIndex].Cells[0].Value.ToString(), "0");

                    }
                    else if (dataShowArea.Rows[RIndex].Cells[CIndex].Value.ToString() == "off")
                    {
                        dataShowArea.Rows[RIndex].Cells[CIndex].Value = "on";
                        WriteSingleCoil("5", dataShowArea.Rows[RIndex].Cells[0].Value.ToString(), "1");
                    }
                }
                else
                {
                    string tempStr = dataShowArea.Rows[RIndex].Cells[CIndex].Value.ToString();
                    Form2 form2 = new Form2(tempStr);
                    form2.Owner = this;
                    form2.Show();


                }
            }
            /*                resetEvent.Set();*/

        }


        #endregion

        #region 读取函数，将读取到的数据放入List中
        /// <summary>
        /// 读输出线圈 功能码01
        /// </summary>
        void ReadCoil(string function, string startAddress, int value, List<dataObject> datalist1)
        {
            //声明buffer数值，用于存储发送字节
            byte[] buffer;
            //调用SendMessage方法，返回发送字节
            buffer = SendMessage(Convert.ToByte(function), Convert.ToUInt16(startAddress), Convert.ToUInt16(value));
            socket.Send(buffer);

            string temp = "[发送]" + BitConverter.ToString(buffer);

            Invoke(sendDel,temp);


            byte[] receiveBuffer = new byte[1024];
            try
            {
                socket.Receive(receiveBuffer);
                
            }
            catch
            {
                MessageBox.Show("出错");
            }

            if (!(receiveBuffer[7] == buffer[7]))
            {
                MessageBox.Show("客户端功能码与服务端功能码不同，请进行检查");
                return;
            }

            int receiveDataLength = (receiveBuffer[4] * 256) + receiveBuffer[5] - 3; //获取返回的有效数据字节数

            byte[] receiveDataBuffer = new byte[receiveDataLength];  //存放接收的有效数据字节
            Array.Copy(receiveBuffer, 9, receiveDataBuffer, 0, receiveDataLength); //receiveBuffer中，前面九个字节为MBAP+功能码+返回的字节数量，所以要从第十个字节开始拿去数据

            BitArray boolArray = new BitArray(receiveDataBuffer);

            for (int i = 0; i < value; i++)
            {
                if (boolArray[i])
                {
                    datalist1.Add(new dataObject() { name = "", address = i + Convert.ToInt32(startAddress), value = "on" });
                }
                else
                {
                    datalist1.Add(new dataObject() { name = "", address = i + Convert.ToInt32(startAddress), value = "off" });
                }
            }
        }

        /// <summary>
        /// 读取寄存器 功能码03
        /// </summary>

        void ReadRegister(string function, string startAddress, int value, List<dataObject> datalist1)
        {
            byte[] buffer;
            buffer = SendMessage(Convert.ToByte(function), Convert.ToUInt16(startAddress), Convert.ToUInt16(value));
            socket.Send(buffer);

            byte[] receiveBuffer = new byte[1024];
            MessageBox.Show(BitConverter.ToString(receiveBuffer));
            socket.Receive(receiveBuffer);

            if (!(receiveBuffer[7] == buffer[7]))
            {
                MessageBox.Show("客户端功能码与服务端功能码不同，请进行检查");
                return;
            }

            int receiveDataLength = (receiveBuffer[4] * 256) + receiveBuffer[5] - 3; //获取返回的有效数据字节数

            byte[] receiveDataBuffer = new byte[receiveDataLength];  //存放接收的有效数据字节

            Array.Copy(receiveBuffer, 9, receiveDataBuffer, 0, receiveDataLength); //receiveBuffer中，前面九个字节为MBAP+功能码+返回的字节数量，所以要从第十个字节开始拿去数据

            //byte[] reverseDataBuffer = receiveDataBuffer.Reverse().ToArray();  //此处没必要反转，因为for里面，展示当前第几位不好与实际数值匹配，for内部直接采取IPAddress.NetWorlToHostOrder进行大小端反转
            for (int i = 0; i < receiveDataLength; i += 2)
            {
                datalist1.Add(new dataObject() { name = "", address = i / 2 + Convert.ToInt32(startAddress), value = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(receiveDataBuffer, i)).ToString() });
            }

        }

        private void ReadAllRegister()
        {
            int round = 525;
            List<dataObject> datalist1 = new List<dataObject>();
            string StartAddress = "0";
            int count = 65535;
            Register(round, datalist1, StartAddress, count);
            showListData(datalist1);
            resetEvent.WaitOne();
            task.Wait(500);
        }

        private void ReadAllCoil()
        {
            List<dataObject> datalist1 = new List<dataObject>();
            int round = 33;
            string StartAddress = "0";
            int count = 65535;
            Coil(round, datalist1, StartAddress, count);
            showListData(datalist1);
            resetEvent.WaitOne();
            task.Wait(500);
        }


        private void ReadWantCountRegister()
        {
            int round;
            if (Convert.ToUInt16(CountTextBox.Text) % 125 != 0)
            {
                round = Convert.ToUInt16(CountTextBox.Text) / 125 + 1;
            }
            else
            {
                round = Convert.ToUInt16(CountTextBox.Text) / 125;
            }
            List<dataObject> datalist1 = new List<dataObject>();
            string StartAddress = StartAddressTextBox.Text;
            int count = Convert.ToUInt16(CountTextBox.Text);

            Register(round, datalist1, StartAddress, count);
            
            showListData(datalist1);
            resetEvent.WaitOne();
            task.Wait(500);
        }



        private void ReadWantCountCoil()
        {
            int round;
            if (Convert.ToUInt16(CountTextBox.Text) % 2000 != 0)
            {
                round = Convert.ToUInt16(CountTextBox.Text) / 2000 + 1;
            }
            else
            {
                round = Convert.ToUInt16(CountTextBox.Text) / 2000;
            }
            List<dataObject> datalist1 = new List<dataObject>();
            string StartAddress = StartAddressTextBox.Text;
            int count = Convert.ToUInt16(CountTextBox.Text);
            Coil(round, datalist1, StartAddress, count);
            showListData(datalist1);
            resetEvent.WaitOne();
            task.Wait(500);
        }

        private void Coil(int round, List<dataObject> datalist1, string StartAddress, int count)
        {
            if (round == 1)
            {
                ReadCoil("1", StartAddress, count, datalist1);
            }
            else
            {
                for (int i = 1; i <= round; i++)
                {
                    if (i == 1)
                    {
                        ReadCoil("1", StartAddress, 2000, datalist1);
                    }
                    else if (i == round)
                    {
                        int tempStartAddress = 2000 * (i - 1) + 1 + Convert.ToUInt16(StartAddress);
                        int tempCount = count - (i - 1) * 2000 - 1;
                        ReadCoil("1", tempStartAddress.ToString(), tempCount, datalist1);
                    }
                    else
                    {
                        int tempStartAddress = 2000 * (i - 1) + 1 + Convert.ToUInt16(StartAddress);

                        ReadCoil("1", tempStartAddress.ToString(), 125, datalist1);
                    }
                }
            }
        }

        private void Register(int round, List<dataObject> datalist1, string StartAddress, int count)
        {
            if (round == 1)
            {
                ReadRegister("3", StartAddress, count, datalist1);
            }
            else
            {
                for (int i = 1; i <= round; i++)
                {
                    if (i == 1)
                    {
                        ReadRegister("3", StartAddress, 125, datalist1);
                    }
                    else if (i == round)
                    {
                        int tempStartAddress = 125 * (i - 1) + 1 + Convert.ToUInt16(StartAddress);
                        int tempCount = count - (i - 1) * 125 - 1;
                        ReadRegister("3", tempStartAddress.ToString(), tempCount, datalist1);
                    }
                    else
                    {
                        int tempStartAddress = 125 * (i - 1) + 1 + Convert.ToUInt16(StartAddress);

                        ReadRegister("3", tempStartAddress.ToString(), 125, datalist1);
                    }
                }
            }
        }

        #endregion

        #region 将List中数据展示出来函数

        void showListData(List<dataObject> list)
        {
            this.Invoke(new Action(() => {
                dataShowArea.DataSource = list;
                dataShowArea.FirstDisplayedScrollingRowIndex = count;
                //设置DataGridView文本居中
                dataShowArea.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }));
        }

        #endregion
        
        #region 写入函数

        /// <summary>
        /// 写入单个线圈 功能码05
        /// </summary>
        void WriteSingleCoil(string function,string startAddress,string value)
        {
            if(Convert.ToUInt16(value) == 1 || Convert.ToUInt16(value) == 0)
            {
                //对于写入线圈coil， 报文 0xff00为置1  0x0000为置0
                byte[] buffer;
                byte[] coilValue = new byte[2];
                if (Convert.ToUInt16(value) == 1)
                {
                    coilValue[1] = 0xff;
                    coilValue[0] = 0x00;
                }
                else if (Convert.ToUInt16(value) == 0)
                {
                    coilValue[1] = 0x00;
                    coilValue[0] = 0x00;
                }

                buffer = SendMessage(Convert.ToByte(function), Convert.ToUInt16(startAddress), BitConverter.ToUInt16(coilValue, 0));
                socket.Send(buffer);
                string temp = BitConverter.ToString(buffer);
                Invoke(sendDel, temp);

                byte[] receiveBuffer = new byte[1024];
                socket.Receive(receiveBuffer);
                int a = 8 + receiveBuffer[8];
                byte[] tempByte = receiveBuffer.Skip(0).Take(a).ToArray();
                string temp1 = "[接收]" + BitConverter.ToString((byte[])tempByte);
                Invoke(sendDel, temp1);

                if (!(receiveBuffer[7] == buffer[7]))
                {
                    MessageBox.Show("客户端功能码与服务端功能码不同，请进行检查");
                    return;
                }
                else
                {
                    MessageBox.Show("写入成功！");
                }

            }
            else
            {
                MessageBox.Show("请输入1或0！");
            }
        }

        /// <summary>
        /// 写单个保持寄存器  功能码06   
        /// </summary>
        public void WriteRegister(string function, string startAddress, string value)
        {
            byte[] buffer;
            buffer = SendMessage(Convert.ToByte(function), Convert.ToUInt16(startAddress), Convert.ToUInt16(value));
            socket.Send(buffer);

            byte[] receiveBuffer = new byte[1024];
            socket.Receive(receiveBuffer);
            int a = 8 + receiveBuffer[8];
            byte[] tempByte = receiveBuffer.Skip(0).Take(a).ToArray();
            string temp1 = "[接收]" + BitConverter.ToString((byte[])tempByte);
            Invoke(sendDel, temp1);
            if (!(receiveBuffer[7] == buffer[7]))
            {
                MessageBox.Show("客户端功能码与服务端功能码不同，请进行检查");
                return;
            }
            else
            {
                MessageBox.Show("写入成功");
            }
        }

        #endregion

        #region 报文生成函数

        /// <summary>
        /// 客户端发送报文
        /// </summary>
        /// <returns></returns>

        // SendMessage（）用来将要发送的信息处理成报文并返回来
        byte[] SendMessage(byte function, ushort startAddress, ushort count)
        {
            byte[] buffer = new byte[12];

            buffer[0] = BitConverter.GetBytes(TPI)[1];
            buffer[1] = BitConverter.GetBytes(TPI)[0];
            buffer[2] = 0x00;
            buffer[3] = 0x00;
            buffer[4] = 0x00;
            buffer[5] = 0x06;
            buffer[6] = 0xFF;
            buffer[7] = function;
            //起始地址
            buffer[8] = BitConverter.GetBytes(startAddress)[1];
            buffer[9] = BitConverter.GetBytes(startAddress)[0];
            //操作个数
            buffer[10] = BitConverter.GetBytes(count)[1];
            buffer[11] = BitConverter.GetBytes(count)[0];

            TPI++;

            return buffer;
        }
        #endregion

        #region 任务委托函数

        private void SendMsg(string str)
        {
            this.textBox1.AppendText(str + "\r\n");
        }

        #endregion
    }

    public class dataObject //将数据制成表格所要用到的类
    {
        public int address { get; set; }
        public string name { get; set; }

        public string value { get; set; }
    }
}
