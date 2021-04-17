using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SSMiniProgram
{
    /// <summary>
    /// https://www.bilibili.com/video/BV1k7411A7p6
    /// .NET Coreר��[Asp.NetCore�������̺�����(����)] -��������
    /// </summary>
    public class Program
    {
        //������ʵ���Կ���.NET Core����һ��Ӧ�ÿ���̨��
        public static void Main(string[] args)
        {
            /*
             - CreateHostBuilder����ֻ�ǰ�������ί�еĶ���ȡ��ŵ�List���棬û��ִ�У�
             - ������ʼִ������Build()������ִ�У�����������������֮ǰ�������ᱻִ�У������������е����ö������Ӻ��ˣ�
             -------------------------------------------
             -һ������������ֻ�ܹ���һ���������ٴι�����ֱ��Throw Exception,���Բο��Ķ�Դ�룻
             -  Run()������������������*/
            CreateHostBuilder(args).Build().Run();
        }

        //������ᴴ��������host),���Ұ�.NET CoreӦ�ð����������棻
        //�����Ǵ���Ĭ������������(��������ҪĿ�ľ������ã������ö�д����֮�� ����Main��������Build().Run();
        //Host������.NET Core����չ�����: .NET Extension, ����չ���ǿ�Դ�ģ�

        //�����Ե��������.NET Extension���棬 ��.NET Core���涼�Ǻ��ĵ�API��


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //������������������������֣�һ�����ǹ����������ã���һ���ǹ���Ӧ�����ã�
            //{.NET Core��һ���������󣬰����˵�ǰӦ�������������Դ��}
            //ͬʱ���ᴴ��Ĭ�ϵķ���������UserDefaultServiceProvider)
            Host.CreateDefaultBuilder(args)
                //.NET Core ������Host��һ���Ƿ��ͣ�ͨ�ã�Host����һ����Web����
                //Web�����Ƿ�����������չ���ṩ�˶���Web���ܱ���֧��HTTP������Kestrel,������IIS���ɵȣ�
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    /*
                     * - ��ǰ׺Ϊ"ASPNETCORE"���ص�WEB��������
                     * - ��Kestrel����ΪWeb���������������Ĭ������ ��/Ҳ֧��IIS���ɣ�������Kestrel�Ƕ�ѡһ��ϵ��
                     * - �����ί������Ҳ���Խ����Զ�������
                     *
                     * -----------------------------------------------------------------------------------------
                     * - С�᣺���ﶼ���ڡ�������á���������������������������������Щ������ö�����չ���ṩ�����÷�����
                     */

                    //�����һ�����ӣ���Kestrel���ý������ã�������������������1024*3����Ĭ����28.6 M);
                    //���������꣬����÷������Ngix����ҲҪ��Ngix�Ͻ����޸������Body���ֵ
                    //webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024);

                    //��������Log�������־����
                   // webBuilder.ConfigureLogging(l=>l.SetMinimumLevel(LogLevel.Debug));


                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:6000");//�ı�˿ں�
                });
    }

    /*ʲô������?
     - ��������Ӧ�õ��������������ڵĹ���
     - ���� ���÷�����
     - ���� ������ܵ�
     - Ĭ��������־��¼
     - ������ϵ��ע�������
     - �������������������������˵����һ����װ��Ӧ����Դ�ġ����󡿣�
     - �����Դ������.NET Core��һ���࣬�����ͽ� Host(�������� ����)

     */
}
