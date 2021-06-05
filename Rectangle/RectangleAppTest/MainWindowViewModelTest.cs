using NUnit.Framework;
using RectangleApp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RectangleAppTest
{
    public class MainWindowViewModelTest
    {
        /// <summary>
        /// Testcasesource for canclear method
        /// </summary>
        public static IEnumerable<TestCaseData> CanClearTestSource
        {
            get
            {
                bool expected = false;
                yield return new TestCaseData(null, expected);
                List<RectangleSizeModel> rectangleModels = new List<RectangleSizeModel>();
                rectangleModels.Add(new RectangleSizeModel());
                expected = true;
                yield return new TestCaseData(rectangleModels, expected);
            }
        }

        /// <summary>
        /// Testcasesource for CanRemove method
        /// </summary>
        public static IEnumerable<TestCaseData> CanRemoveTestSource
        {
            get
            {
                bool expected = false;
                yield return new TestCaseData(null, expected);
                expected = true;
                yield return new TestCaseData(new RectangleSizeModel(), expected);
            }
        }

        /// <summary>
        /// Testcasesource for CanAdd method
        /// </summary>
        public static IEnumerable<TestCaseData> CanAddTestSource
        {
            get
            {
                bool expected = false;
                yield return new TestCaseData(null, 1, expected);
                expected = false;
                yield return new TestCaseData(new List<RectangleSizeModel>(),0 , expected);
                yield return new TestCaseData(new List<RectangleSizeModel> { new RectangleSizeModel()},
                    0, expected);
                expected = true;
                yield return new TestCaseData(new List<RectangleSizeModel> { new RectangleSizeModel() },
                    2, expected);
            }
        }

        /// <summary>
        /// Testcasesource for ExecuteClearCommand method
        /// </summary>
        public static IEnumerable<TestCaseData> ClearCmdTestSource
        {
            get
            {
                yield return new TestCaseData(new List<RectangleSizeModel> 
                { new RectangleSizeModel() }, 0);
            }
        }

        /// <summary>
        /// Testcasesource for ExecuteRemoveCommand method
        /// </summary>
        public static IEnumerable<TestCaseData> RemoveCmdTestSource
        {
            get
            {
                yield return new TestCaseData(new RectangleSizeModel() , false);
            }
        }

        /// <summary>
        /// Testcasesource for ExecuteAddCommand method
        /// </summary>
        public static IEnumerable<TestCaseData> AddCmdTestSource
        {
            get
            {
                yield return new TestCaseData(10, 10, 1);
                yield return new TestCaseData(-1, 10, 0);
                yield return new TestCaseData(1, -1, 0);
                yield return new TestCaseData(ApplicationConstants.MaxSize +1, 10, 0);
                yield return new TestCaseData(10, ApplicationConstants.MaxSize +1, 0);
            }
        }

        /// <summary>
        /// Testcasesource for ExecuteSubmitCommand method
        /// </summary>
        public static IEnumerable<TestCaseData> SubmitCmdTestSource
        {
            get
            {
                yield return new TestCaseData(10, 10);
                yield return new TestCaseData(0, 0);
                yield return new TestCaseData(-1, 0);
                yield return new TestCaseData(ApplicationConstants.MaxRectangles + 1, 0);
            }
        }

        /// <summary>
        /// Testcasesource for ExecuteSubmitCommand method
        /// </summary>
        public static IEnumerable<TestCaseData> SquarePercentageTestSource
        {
            get
            {
                yield return new TestCaseData(0, null, 0);
                yield return new TestCaseData(0,
                    new ObservableCollection<RectangleSizeModel> 
                    { new RectangleSizeModel { 
                        Height = 10, Width = 10
                    } }, 0);
                yield return new TestCaseData(1,
                    new ObservableCollection<RectangleSizeModel>
                    { new RectangleSizeModel {
                        Height = 10, Width = 10
                    } }, 100);
                yield return new TestCaseData(1,
                    new ObservableCollection<RectangleSizeModel>
                    { new RectangleSizeModel {
                        Height = 10, Width = 100
                    } }, 0);
                yield return new TestCaseData(2,
                    new ObservableCollection<RectangleSizeModel>
                    { new RectangleSizeModel {
                        Height = 10, Width = 100
                    },
                    new RectangleSizeModel {
                        Height = 10, Width = 10
                    }}, 50);
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test method to test CanClear method
        /// </summary>
        /// <param name="rectangleCollection">Rectangle sizes</param>
        /// <param name="expectedValue">Expected value</param>
        [Test, TestCaseSource(nameof(CanClearTestSource))]
        public void CanClearTest(List<RectangleSizeModel> rectangleCollection, bool expectedValue)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            if (rectangleCollection == null)
            {
                mainWindowViewModel.RectangleSizes = null;
            }
            else
            {
                mainWindowViewModel.RectangleSizes = new ObservableCollection<RectangleSizeModel>(rectangleCollection);
            }
            var actualResult = mainWindowViewModel.ClearCmd.CanExecute(null);
            Assert.AreEqual(expectedValue, actualResult);
        }

        /// <summary>
        /// Test method to test CanRemove method
        /// </summary>
        /// <param name="selectedRectangleSize">selectedRectangleSize</param>
        /// <param name="expectedValue">Expected value</param>
        [Test, TestCaseSource(nameof(CanRemoveTestSource))]
        public void CanRemoveTest(RectangleSizeModel selectedRectangleSize, bool expectedValue)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.SelectedRectangleSize = selectedRectangleSize;
            var actualResult = mainWindowViewModel.RemoveCmd.CanExecute(null);
            Assert.AreEqual(expectedValue, actualResult);
        }

        /// <summary>
        /// Test method to test CanAdd method
        /// </summary>
        /// <param name="rectangleSizeModels"><selectedRectangleSize/param>
        /// <param name="numberRectangles">Number Rectangles</param>
        /// <param name="expectedValue">Expected value</param>
        [Test, TestCaseSource(nameof(CanAddTestSource))]
        public void CanAddTest(List<RectangleSizeModel> rectangleSizeModels,
            int numberRectangles, bool expectedValue)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            if (rectangleSizeModels == null)
            {
                mainWindowViewModel.RectangleSizes = null;
            }
            else
            {
                mainWindowViewModel.RectangleSizes = new ObservableCollection<RectangleSizeModel>(rectangleSizeModels);
            }
            mainWindowViewModel.NumberOfRectangles = numberRectangles;
            var actualResult = mainWindowViewModel.AddCmd.CanExecute(null);
            Assert.AreEqual(expectedValue, actualResult);
        }

        /// <summary>
        /// Test method to test ExecuteClearCommand method
        /// </summary>
        /// <param name="rectangleSizeModels"> Rectangle sizes</param>
        /// <param name="expectedValue">Expected value</param>
        [Test, TestCaseSource(nameof(ClearCmdTestSource))]
        public void ExecuteClearCommandTest(List<RectangleSizeModel> rectangleSizeModels,
            int expectedValue)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            if (rectangleSizeModels == null)
            {
                mainWindowViewModel.RectangleSizes = null;
            }
            else
            {
                mainWindowViewModel.RectangleSizes = new ObservableCollection<RectangleSizeModel>(rectangleSizeModels);
            }
            mainWindowViewModel.ClearCmd.Execute(null);
            Assert.AreEqual(expectedValue, mainWindowViewModel.RectangleSizes.Count);
        }

        /// <summary>
        /// Test method to test ExecuteRemoveCommand method
        /// </summary>
        /// <param name="selectedRectangleSize">selectedRectangleSize</param>
        /// <param name="expectedValue">Expected value</param>
        [Test, TestCaseSource(nameof(RemoveCmdTestSource))]
        public void ExecuteRemoveCommandTest(RectangleSizeModel selectedRectangleSize,
            bool expectedValue)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            mainWindowViewModel.RectangleSizes.Add(selectedRectangleSize);
            mainWindowViewModel.SelectedRectangleSize = selectedRectangleSize;
            mainWindowViewModel.RemoveCmd.Execute(null);
            Assert.AreEqual(expectedValue, mainWindowViewModel.RectangleSizes.Contains(selectedRectangleSize));
        }

        /// <summary>
        /// Test method to test ExecuteAddCommand method
        /// </summary>
        /// <param name="length">Length of rectangle</param>
        /// <param name="width">width of rectangle</param>
        /// <param name="expectedCount">Expected Count</param>
        [Test, TestCaseSource(nameof(AddCmdTestSource))]
        public void ExecuteAddCommandTest(int length, int width,
            int expectedCount)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            mainWindowViewModel.LengthInput = length;
            mainWindowViewModel.WidthInput = width;
            mainWindowViewModel.AddCmd.Execute(null);
            Assert.AreEqual(expectedCount, mainWindowViewModel.RectangleSizes.Count);
        }

        /// <summary>
        /// Test method to test ExecuteAddCommand method
        /// </summary>
        /// <param name="numberOfRectangles">Number of rectangle</param>
        /// <param name="expectedCount">Expected Count</param>
        [Test, TestCaseSource(nameof(SubmitCmdTestSource))]
        public void ExecuteSubmitCommandTest(int numberOfRectangles,
            int expectedCount)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            mainWindowViewModel.NumberOfRectangles = numberOfRectangles;
            mainWindowViewModel.IsOperationInProgress = true;
            mainWindowViewModel.SubmitCmd.Execute(null);
            while (mainWindowViewModel.IsOperationInProgress) ;
            Assert.AreEqual(expectedCount, mainWindowViewModel.Rectangles.Count);
        }

        /// <summary>
        /// Test method to test UpdateSquarePercentage method
        /// </summary>
        /// <param name="numberOfRectangles">Number of rectangle</param>
        /// <param name="rectangleSizeModels">RectangleSizes</param>
        /// <param name="expectedPercentage">Expected Percentage</param>
        [Test, TestCaseSource(nameof(SquarePercentageTestSource))]
        public void UpdateSquarePercentageTest(int numberOfRectangles,
            ObservableCollection<RectangleSizeModel> rectangleSizeModels,
            int expectedPercentage)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            mainWindowViewModel.NumberOfRectangles = numberOfRectangles;
            if (rectangleSizeModels?.Count > 0)
            {
                mainWindowViewModel.RectangleSizes = rectangleSizeModels;
            }
            mainWindowViewModel.IsOperationInProgress = true;
            mainWindowViewModel.SubmitCmd.Execute(null);
            while (mainWindowViewModel.IsOperationInProgress) ;
            Assert.AreEqual(expectedPercentage, mainWindowViewModel.SquarePercentage);
        }
    }
}