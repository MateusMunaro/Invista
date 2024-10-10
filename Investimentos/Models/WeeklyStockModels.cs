using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Investimentos.Models
{
  public class WeeklyStockData
  {
    public string Symbol { get; set; }
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public long Volume { get; set; }

  }
}