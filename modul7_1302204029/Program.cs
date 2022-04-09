using System;
using System.Text.Json;

namespace modul7_1302204029
{
    class Program
    {
        static void Main(string[] args)
        {
            isiBankTransferConfig konfigTransfer = new isiBankTransferConfig();
            Console.WriteLine("lang: (en/id)");
            string pilBahasa = Console.ReadLine();
            if (pilBahasa == "en")
            {
                Console.WriteLine(konfigTransfer.conf.confirmation.en);
            }
            else
            {
                Console.WriteLine(konfigTransfer.conf.confirmation.id);
            }
        }
    }
    class isiBankTransferConfig
    {
        public BankTransferConfig conf;
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public string fileConfigName = "bank_transfer_config.json";
        public isiBankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch
            {
                SetDefault();
                WriteConfigFile();
            }
        }
        private BankTransferConfig ReadConfigFile()
        {
            string jsonStringFromFile = File.ReadAllText(path +"/"+ fileConfigName);
            conf = JsonSerializer.Deserialize<BankTransferConfig>(jsonStringFromFile);
            return conf;
        }
        private void WriteConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(conf, options);
            string fullFilePath = path + "/" + fileConfigName;
            File.WriteAllText(fullFilePath, jsonString);
        }
        private void SetDefault()
        {
            confirmationConfig objconfirmationConfig = new confirmationConfig(
                "Please insert the amount of money to transfer:",
                "Masukkan jumlah uang yang akan di transfer:");
            transferConfig objtransferConfig = new transferConfig(
                "25000000",
                "6500",
                "15000");
        }

    }
    class BankTransferConfig
    {
        public transferConfig transfer { get; set; }
        public string lang { get; set; }
        public confirmationConfig confirmation { get; set; }
        public string methods { get; set; }
        public BankTransferConfig() { }
        public BankTransferConfig(transferConfig transfer, string lang, confirmationConfig confirmation, string methods)
        {
            this.transfer = transfer;
            this.lang = lang;
            this.confirmation = confirmation;
            this.methods = methods;
        }

    }
    class transferConfig
    {
        public string threshold { get; set; }
        public string low_fee { get; set; }
        public string high_fee { get; set; }
        public transferConfig() { }
        public transferConfig(string threshold, string low_fee, string high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    class confirmationConfig
    {
        public string en { get; set; }
        public string id { get; set; }
        public confirmationConfig() { }
        public confirmationConfig(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }
}
