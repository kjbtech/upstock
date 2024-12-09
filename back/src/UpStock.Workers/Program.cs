// See https://aka.ms/new-console-template for more information

using Mindee;
using Mindee.Input;
using Mindee.Product.FinancialDocument;

string folder = "D:\\data\\upstock\\";
string filePath = "Logiciel-electricien-devis.jpg";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient("6effc709f6d3aa733bf72f188a4091da");

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(folder + filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<FinancialDocumentV1>(inputSource);

// Print a summary of all the predictions
Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
Console.WriteLine(response.Document.Inference.Prediction.LineItems.ToString());

Console.WriteLine("Hello, World!");
