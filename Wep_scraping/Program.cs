using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace Wep_scraping
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var apiKey = "XYMF8BQUEKC6QW0DHJ1VEUYWSD5KZ8T1XZ97RWISSFWQ32MOHDJKILSTP3DRI19QC0F4CLHMNC40JASC";
            var url = "https://en.wikipedia.org/wiki/OpenAI";
            var apiUrl = $"https://app.scrapingbee.com/api/v1/?api_key={apiKey}&url={url}&premium_proxy=true";//&render_js=true
            var httpClint = new HttpClient();
            var html = await httpClint.GetStringAsync(apiUrl);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var divs = htmlDoc.DocumentNode.Descendants("div").
                Where(node => node.GetAttributeValue("class", "").Contains("mw-parser-output")).ToList();

            foreach (var div in divs)
            {
                //Console.WriteLine(div.InnerText.Trim());
                var paragraph = div.Descendants("p").Where(p => !string.IsNullOrWhiteSpace(p.InnerText)).ToList();
                if(paragraph.Count>0)
                {
                    Console.WriteLine(paragraph[0].InnerText.Trim());
                    break;
                }

            }
        }
    }
}
