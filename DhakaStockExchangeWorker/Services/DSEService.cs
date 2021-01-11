using DhakaStockExchangeWorker.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DhakaStockExchangeWorker.Services
{
    public class DSEService : IDSEService
    {
        private const string URL = "https://www.dse.com.bd/latest_share_price_scroll_l.php";

        private HtmlWeb WEB = new HtmlWeb();
        private HtmlDocument HTMLDOC = null;

        private readonly IDSERepository _repository;

        public DSEService(IDSERepository repository)
        {
            WEB.PreRequest += request =>
            {
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };

            HTMLDOC = WEB.Load(URL);

            _repository = repository;
        }

        public void InsertData()
        {
            var table = GetTable();
            if (table == null)
                throw new NotFoundException("Error occured, when get table");

            var rows = GetAllRows(table).ToList();
            if(rows == null)
                throw new NotFoundException("No rows found!");

            var allTds = GetAllTd(rows);
            if(allTds == null)
                throw new NotFoundException("No td found!");

            var insertableData = ConvertTdsToDSEModelObject(allTds).ToList();
            if(insertableData == null)
                throw new NotFoundException("Error occured, when convert td to DSEModel object");

            _repository.InsertDatas(insertableData);
            _repository.Save();
        }

        public bool IsMarketOpen()
        {
            var span = HTMLDOC.DocumentNode.SelectNodes("//div[contains(@class, 'containbox')]//header[contains(@class, 'Header')]//span[contains(@class, 'time')]//span").FirstOrDefault();
            if (span.InnerText.ToUpper() == "open".ToUpper() || span.InnerText.ToUpper() == "opened".ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private HtmlNode GetTable()
        {
            HtmlNode table = HTMLDOC.DocumentNode.SelectNodes("//section//table[contains(@class, 'shares-table')]").FirstOrDefault();

            return table;

        }

        private IList<HtmlNode> GetAllRows(HtmlNode table)
        {
            var htmlRows = new List<HtmlNode>();

            var firstRow = table.SelectSingleNode("tbody//tr");
            htmlRows.Add(firstRow);

            foreach (HtmlNode row in table.SelectNodes("tr"))
            {
                htmlRows.Add(row);
            }

            return htmlRows;
        }

        private HtmlNodeCollection GetRowTds(HtmlNode row)
        {
            var cells = row.SelectNodes("td");
            return cells;
        }

        private IList<HtmlNodeCollection> GetAllTd(List<HtmlNode> nodes)
        {
            var tds = new List<HtmlNodeCollection>();

            foreach (var node in nodes)
            {
                var td = GetRowTds(node);
                tds.Add(td);
            }

            return tds;
        }

        private IList<DSEModel> ConvertTdsToDSEModelObject(IList<HtmlNodeCollection> nodes)
        {
            var listsData = new List<DSEModel>();

            foreach (var node in nodes)
            {
                var data = ConvertCellToDSEModelObjecs(node);
                listsData.Add(data);
            }

            return listsData;
        }

        public DSEModel ConvertCellToDSEModelObjecs(HtmlNodeCollection cells)
        {
            var value = cells[1].InnerText;
            var replacement = Regex.Replace(value, @"\t|\n", "");

            var data = new DSEModel
            {
                SerialNo = Guid.NewGuid(),
                TrandingCode = replacement
            };

            long id;
            var idValue = RemoveComma(cells[0].InnerHtml);
            long.TryParse(idValue, out id);

            double ltp;
            var ltpValue = RemoveComma(cells[2].InnerText);
            double.TryParse(ltpValue, out ltp);

            double high;
            var highValue = RemoveComma(cells[3].InnerText);
            double.TryParse(highValue, out high);

            double low;
            var lowValue = RemoveComma(cells[4].InnerText);
            double.TryParse(lowValue, out low);

            double closep;
            var closepValue = RemoveComma(cells[5].InnerText);
            double.TryParse(closepValue, out closep);

            double ycp;
            var ycpValue = RemoveComma(cells[6].InnerText);
            double.TryParse(ycpValue, out ycp);

            double change;
            var changeValue = RemoveComma(cells[7].InnerText);
            double.TryParse(changeValue, out change);

            long trade;
            var tradeValue = RemoveComma(cells[8].InnerText);
            long.TryParse(tradeValue, out trade);

            double valueData;
            var valueDataValue = RemoveComma(cells[9].InnerText);
            var result = double.TryParse(valueDataValue, out valueData);

            long volume;
            var volumeValue = RemoveComma(cells[10].InnerText);
            long.TryParse(volumeValue, out volume);

            data.Id = id;
            data.LTP = ltp;
            data.High = high;
            data.Low = low;
            data.Closep = closep;
            data.YCP = ycp;
            data.Change = change;
            data.Trade = trade;
            data.Value = valueData;
            data.Volume = volume;

            return data;
        }

        private string RemoveComma(string value)
        {
            var data = value.Replace(",", "");
            return data;

        }
    }
}
