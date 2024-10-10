using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Investimentos.Services
{
  public class FinancialApiService
  {
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly string _apiKey = "TM6LEK536XTQU554"; // Mova isso para configuração em produção

    public FinancialApiService()
    {
      // Qualquer configuração adicional para o HttpClient pode ser definida aqui
    }

    public async Task<JObject> GetTimeSeriesData(string symbol, string outputsize = "compact", string datatype = "json")
    {
      string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&outputsize={outputsize}&datatype={datatype}&apikey={_apiKey}";

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
          var content = await response.Content.ReadAsStringAsync();
          return JObject.Parse(content);
        }
        else
        {
          throw new Exception($"Erro ao chamar a API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
      }
      catch (HttpRequestException httpEx)
      {
        throw new Exception("Erro de conexão com a API. Verifique sua conexão de internet.", httpEx);
      }
      catch (Exception ex)
      {
        throw new Exception("Erro ao processar a resposta da API.", ex);
      }
    }
  }
} 