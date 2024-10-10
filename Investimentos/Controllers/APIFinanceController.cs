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
          var timeSeries = data["Time Series (Daily)"];

          string symbolFromApi = metaData["2. Symbol"].ToString();
          foreach (var dateEntry in timeSeries)
          {
            var date = DateTime.Parse(dateEntry.Path); // Data como chave
            var stockInfo = (JObject)dateEntry.First; // Valores diários

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
          // Verifica se a mensagem de erro indica que o limite foi atingido
          if (ex.Message.Contains("rate limit"))
          {
            ModelState.AddModelError("", "Você atingiu o limite de solicitações da API. Tente novamente mais tarde.");
            return View("Error"); // Retorne uma view de erro ou uma página apropriada
          }
          else
          {
            // Outros erros
            ModelState.AddModelError("", $"Erro ao processar o símbolo {symbol}: {ex.Message}");
          }
        }
      }

      return View("Stock", stockDataList); // Retornando a View Stock.cshtml
    }


  }
}