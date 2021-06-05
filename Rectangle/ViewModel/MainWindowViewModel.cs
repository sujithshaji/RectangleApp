#region Namespace
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
#endregion

namespace RectangleApp
{
    #region Class MainWindowViewModel
    /// <summary>
    /// Viewmodel for MainWindowView
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        private int _numberOfRectangles = 0;
        /// <summary>
        /// Input number of rectangles to generate
        /// </summary>
        public int NumberOfRectangles
        {
            get { return _numberOfRectangles; }
            set
            {
                _numberOfRectangles = value;
                OnPropertyChanged();
            }
        }

        private Command _submitCmd;
        /// <summary>
        /// Submit/Create rectangle command
        /// </summary>
        public Command SubmitCmd
        {
            get { return _submitCmd; }
            set { _submitCmd = value; }
        }

        private ObservableCollection<RectangleModel> _rectangles;
        /// <summary>
        /// Generated rectangles obj collection
        /// </summary>
        public ObservableCollection<RectangleModel> Rectangles
        {
            get { return _rectangles; }
            set
            {
                _rectangles = value;
                OnPropertyChanged();
            }
        }

        private double _squarePercentage = 0;
        /// <summary>
        /// Percentage of squares out of total rectangles
        /// </summary>
        public double SquarePercentage
        {
            get { return _squarePercentage; }
            set
            {
                _squarePercentage = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RectangleSizeModel> _rectangleSizes;
        /// <summary>
        /// Input rectangle size in width & length
        /// </summary>
        public ObservableCollection<RectangleSizeModel> RectangleSizes
        {
            get { return _rectangleSizes; }
            set
            {
                _rectangleSizes = value;
                OnPropertyChanged();
            }
        }

        private RectangleSizeModel _selectedRectangleSize;
        /// <summary>
        /// Selected rectangle size in the list
        /// </summary>
        public RectangleSizeModel SelectedRectangleSize
        {
            get { return _selectedRectangleSize; }
            set
            {
                _selectedRectangleSize = value;
                OnPropertyChanged();
            }
        }

        private int _lengthInput;
        /// <summary>
        /// Input length of rectangle
        /// </summary>
        public int LengthInput
        {
            get { return _lengthInput; }
            set
            {
                _lengthInput = value;
                OnPropertyChanged();
            }
        }

        private int _widthInput;
        /// <summary>
        /// Input width of rectangle
        /// </summary>
        public int WidthInput
        {
            get { return _widthInput; }
            set
            {
                _widthInput = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Add rectangle size to list
        /// </summary>
        public Command AddCmd { get; set; }

        /// <summary>
        /// Remove selected rectangle size from list
        /// </summary>
        public Command RemoveCmd { get; set; }

        /// <summary>
        /// Clear all rectangle size from list
        /// </summary>
        public Command ClearCmd { get; set; }

        /// <summary>
        /// Random value generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Operation status property.
        /// USed to check rectangle creation is in progress or not.
        /// </summary>
        public bool IsOperationInProgress { get; set; } = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// Initialize properties and commands
        /// </summary>
        public MainWindowViewModel()
        {
            SubmitCmd = new Command(ExecuteSubmitCommand, (obj) => true);
            AddCmd = new Command(ExecuteAddCommand, CanAddSize);
            RemoveCmd = new Command(ExecuteRemoveCommand, CanRemove);
            ClearCmd = new Command(ExecuteClearCommand, CanClear);
            NumberOfRectangles = 0;
            Rectangles = new ObservableCollection<RectangleModel>();
            RectangleSizes = new ObservableCollection<RectangleSizeModel>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Check whether rectangle input size list can clear or not
        /// Enabled when list conatins any items
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanClear(object obj)
        {
            return RectangleSizes?.Count > 0;
        }

        /// <summary>
        /// Check Remove button enable/disable
        /// Button will enable when any of the item selected from the list
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanRemove(object obj)
        {
            return SelectedRectangleSize != null;
        }

        /// <summary>
        /// Check Add button can enable or not.
        /// It allowes addition only when the list count should be less than number of rectangles needs to create
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanAddSize(object obj)
        {
            return RectangleSizes?.Count < NumberOfRectangles && RectangleSizes?.Count < ApplicationConstants.MaxRectangles;
        }

        /// <summary>
        /// Command action method to clear the RectangleSizes collection
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteClearCommand(object obj)
        {
            RectangleSizes?.Clear();
        }

        /// <summary>
        /// Command action method to remove the selected rectanglesize item form RectangleSizes collection
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteRemoveCommand(object obj)
        {
            if (SelectedRectangleSize != null)
            {
                RectangleSizes.Remove(SelectedRectangleSize);
            }
        }

        /// <summary>
        /// Command action method to add a new rectanglesize item to RectangleSizes collection
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteAddCommand(object obj)
        {
            if (LengthInput >= 0 &&
                LengthInput <= ApplicationConstants.MaxSize &&
                WidthInput >= 0 &&
                WidthInput <= ApplicationConstants.MaxSize &&
                RectangleSizes?.Count <= NumberOfRectangles)
            {
                RectangleSizes.Add(new RectangleSizeModel
                {
                    Height = LengthInput,
                    Width = WidthInput
                });
            }
        }

        /// <summary>
        /// Command action method to create rectangles and add to Rectangles collection
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecuteSubmitCommand(object obj)
        {
            if (NumberOfRectangles > 0 && NumberOfRectangles <= ApplicationConstants.MaxRectangles)
            {
                IsOperationInProgress = true;
                Rectangles.Clear();
                //Create Task for Rectangle creation
                Task<List<RectangleModel>> task = new Task<List<RectangleModel>>(() => CreateRectangles());
                task.Start();
                var list = await task;
                Rectangles = new ObservableCollection<RectangleModel>(list);
                //Calcualte square type rectangle's percentage after all rectangles created
                UpdateSquarePercentage();
            }
            IsOperationInProgress = false;
        }

        /// <summary>
        /// Implementation for rectangle collection creation
        /// Invoked from through Task
        /// </summary>
        /// <returns>Rectangle collection</returns>
        private List<RectangleModel> CreateRectangles()
        {
            var rectangles = new List<RectangleModel>();
            //Rectangles will create upto the count entered.
            for (int number = 0; number < NumberOfRectangles; number++)
            {
                var rectange = new RectangleModel()
                {
                    Width = GetWidth(number),
                    Height = GetHeight(number)
                };
                rectangles.Add(rectange);
            }
            return rectangles;
        }

        /// <summary>
        /// Get Height of each rectangle.
        /// it will return user selected height for the respective rectangle.
        /// If user selected value is not available, then it will return a random value
        /// </summary>
        /// <param name="index">Rectangle position</param>
        /// <returns></returns>
        private double GetHeight(int index)
        {
            //Return user selected height if available
            if (index < RectangleSizes.Count)
            {
                return RectangleSizes[index].Height;
            }
            //return random height
            else
            {
                return GetRandomNumber(0, ApplicationConstants.MaxSize);
            }
        }

        /// <summary>
        /// Get Width of each rectangle.
        /// it will return user selected width for the respective rectangle.
        /// If user selected value is not available, then it will return a random value
        /// </summary>
        /// <param name="index">Rectangle position</param>
        /// <returns></returns>
        private double GetWidth(int index)
        {
            //Return user selected width if available
            if (index < RectangleSizes.Count)
            {
                return RectangleSizes[index].Width;
            }
            //return random height
            else
            {
                return GetRandomNumber(0, ApplicationConstants.MaxSize);
            }
        }

        /// <summary>
        /// Calculate total squares type rectangles available in the total generated rectangles.
        /// </summary>
        private void UpdateSquarePercentage()
        {
            if (Rectangles?.Count > 0)
            {
                var sqrCount = Rectangles.Count(c => c.RectangleType == RectangleType.Square);
                if (sqrCount < 0) sqrCount = 0;
                var totalCount = Rectangles.Count;
                SquarePercentage = ((double)sqrCount / (double)totalCount) * 100;
            }
        }

        /// <summary>
        /// Return a random integer value between the min & max value range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
    #endregion
}
