using System;
using System.IO;
using ZXing;
using ZXing.QrCode;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("1. Barcode Oluştur");
        Console.WriteLine("2. Barcode Oku");
        Console.Write("İşlem seçin (1 veya 2): ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Oluşturulacak metni girin: ");
            string textToEncode = Console.ReadLine();

            GenerateBarcode(textToEncode);
        }
        else if (choice == "2")
        {
            Console.Write("Okunacak barcode'un dosya yolunu girin: ");
            string filePath = Console.ReadLine();

            ReadBarcode(filePath);
        }
        else
        {
            Console.WriteLine("Geçersiz seçenek.");
        }
    }

    static void GenerateBarcode(string textToEncode)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = 200,
                Width = 200
            }
        };

        var barcodeBitmap = writer.Write(textToEncode);
        Console.WriteLine("Barcode başarıyla oluşturuldu.");

        string filePath = "generated_barcode.png"; // Dosya yolu ve adı
        barcodeBitmap.Save(filePath);

        Console.WriteLine("Barcode dosyaya kaydedildi: " + filePath);
    }

    static void ReadBarcode(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Belirtilen dosya mevcut değil.");
            return;
        }

        var barcodeBitmap = new Bitmap(filePath);
        var reader = new BarcodeReader();

        var result = reader.Decode(barcodeBitmap);

        if (result != null)
        {
            Console.WriteLine("Okunan metin: " + result.Text);
        }
        else
        {
            Console.WriteLine("Barcode okunamadı.");
        }
    }
}
