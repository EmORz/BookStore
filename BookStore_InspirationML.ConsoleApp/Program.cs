//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using BookStore_InspirationML.Model.DataModels;


namespace BookStore_InspirationML.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"C:\Users\User\Desktop\book_price.csv";

        static void Main(string[] args)
        {
        

            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            if (!File.Exists(MODEL_FILEPATH))
            {
                ModelBuilder.CreateModel();
            }

            var listOfInputs = new List<ModelInput>()
            {
                new ModelInput()
                {
                    Book_title = "Beauty Prize",
                    Book_details = "diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam quis turpis eget elit sodales scelerisque mauris",
                    Book_isbn = "004650860-0",
                    Book_publisheDate = "09/11/2018"


                },
                new ModelInput()
                {
                    Book_title = "Yogi Bear",
                    Book_details = "aliquam convallis nunc proin at turpis a pede posuere nonummy integer non velit donec",
                    Book_isbn = "027199853-9",
                    Book_publisheDate = "10/14/2018"


                }
            };

            TestModel(MODEL_FILEPATH, listOfInputs);
            // Training code used by ML.NET CLI and AutoML to generate the model
            ////ModelBuilder.CreateModel();

            //ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            //var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            //// Create sample data to do a single prediction with it 
            //ModelInput sampleData = CreateSingleDataSample(mlContext, DATA_FILEPATH);

            //// Try a single prediction
            //ModelOutput predictionResult = predEngine.Predict(sampleData);

            //Console.WriteLine($"Single Prediction --> Actual value: {sampleData.Book_price} | Predicted value: {predictionResult.Score}");

            //Console.WriteLine("=============== End of process, hit any key to finish ===============");
            //Console.ReadKey();
        }

        private static void TestModel(string modelFilepath, List<ModelInput> listOfInputs)
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(modelFilepath, out _);
            var predictionEnginePool = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            foreach (var currentItem in listOfInputs)
            {
                var predict = predictionEnginePool.Predict(currentItem);
                Console.WriteLine($"Make: {currentItem.Book_title}");
                Console.WriteLine($"Price of Predict: {predict.Score}");
            }
        }

        // Method to load single row of data to try a single prediction
        // You can change this code and create your own sample data here (Hardcoded or from any source)
        private static ModelInput CreateSingleDataSample(MLContext mlContext, string dataFilePath)
        {
            // Read dataset to get a single row for trying a prediction          
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
