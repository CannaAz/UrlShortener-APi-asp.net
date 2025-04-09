

using Microsoft.IdentityModel.Tokens;
using QRCoder;

namespace URLSHORTENER;

public class QrGeneratorService
{
    public string GenerateQRCode(string text)
    {
        if(text.IsNullOrEmpty()) return "";

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        var QRCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        var imageType = Base64QRCode.ImageType.Png;

        var qrCode = new Base64QRCode(QRCodeData);

        string qrCodeImageAsBase64 = qrCode.GetGraphic(7, "Black", "White", true, imageType);


        return string.Format($"data:image/png;base64,{qrCodeImageAsBase64}");
    }
}
