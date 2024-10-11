using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Investimentos.Models;
using Investimentos.Services;
using System;
using Newtonsoft.Json.Linq;

namespace Investimentos.Controllers
{
  public class FinanceController : Controller
  {
    private readonly FinancialApiService _financialApiService;

    public FinanceController()
    {
      _financialApiService = new FinancialApiService();
    }

    public ActionResult Cripto()
    {
      return View("Cripto"); // Certifique-se de que a view Cripto.cshtml esteja na pasta Finance
    }

    
    public async Task<ActionResult> Index()
    {
      var symbols = new List<string> { "IBM", "AAPL", "GOOGL" }; // Adicione os símbolos desejados
      var stockDataList = new List<WeeklyStockData>();

      foreach (var symbol in symbols)
      {
        try
        {
          var data = await _financialApiService.GetTimeSeriesData(symbol);

          // Processando "Meta Data" e "Time Series (Daily)"
          var metaData = data["Meta Data"];
          var timeSeries = (JObject)data["Time Series (Daily)"]; // Aqui, converta para JObject

          string symbolFromApi = metaData["2. Symbol"].ToString();

          // Iterar sobre as propriedades do timeSeries
          foreach (var dateEntry in timeSeries.Properties())
          {
            var date = DateTime.Parse(dateEntry.Name); // A chave (Name) é a data
            var stockInfo = (JObject)dateEntry.Value; // O valor é um JObject com os valores diários

            // Adicionar os dados à lista
            stockDataList.Add(new WeeklyStockData
            {
              Symbol = symbolFromApi, // Pega o símbolo da Meta Data
              Date = date,
              Open = decimal.Parse(stockInfo["1. open"].ToString()),
              High = decimal.Parse(stockInfo["2. high"].ToString()),
              Low = decimal.Parse(stockInfo["3. low"].ToString()),
              Close = decimal.Parse(stockInfo["4. close"].ToString()),
              Volume = long.Parse(stockInfo["5. volume"].ToString())
            });
          }
        }
        catch (Exception ex)
        {
          // Tratar erros se necessário
          Console.WriteLine(ex.Message); // Log de erro
        }
      }

      return View("Stock", stockDataList); // Retornando a View Stock.cshtml
    }


  }
}