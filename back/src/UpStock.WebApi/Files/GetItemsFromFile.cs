using Mindee.Input;
using Mindee.Product.FinancialDocument;
using Mindee;
using UpStock.Domain.Items;
using UpStock.Domain;
using UpStock.Domain.Files;

namespace UpStock.WebApi.Files;

public class GetItemsFromFile
{
    private readonly MindeeClient _mindeeClient = new("6effc709f6d3aa733bf72f188a4091da");
    private readonly IUpstockRepository _upstockRepository;

    public GetItemsFromFile(IUpstockRepository upstockRepository)
    {
        _upstockRepository = upstockRepository;
    }

    public async Task ExecuteAsync(FileToAnalyze fileToAnalyze)
    {
        await ExtractAsync(fileToAnalyze.Uri);
    }

    private async Task ExtractAsync(Uri uri)
    {
        // l'url à atteindre doit être en direct et ne doit pas proposer une redirection
        var response = await _mindeeClient
            .ParseAsync<FinancialDocumentV1>(new UrlInputSource(uri));

        foreach (var lineItem in response.Document.Inference.Prediction.LineItems)
        {
            var newItem = new ExtractedFromFile(
                Guid.NewGuid().ToString(),
                lineItem.Description,
                lineItem.ProductCode,
                lineItem.Quantity,
                null
                );

            await _upstockRepository.StoreAsync<Item, ExtractedFromFile>(newItem.ItemId, newItem);
        }
    }
}
