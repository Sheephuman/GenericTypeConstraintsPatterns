using HeatResponseToken.ErrorData;
using HeatResponseToken.Models;
using HeatResponseToken.ViewModel;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace HeatResponseToken.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private const string JsonFilePath = "TestData.json";

        public ObservableCollection<ValidationToken<HeatResponseRecord>> Records { get; }
       = new();

        public DelegateCommand ValidateAndRecordCommand { get; }

        public DelegateCommand ErrorRecordCommand { get; }

        private string _location = string.Empty;

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private DateTime? _recordedAt = DateTime.Now;

        public DateTime? RecordedAt
        {
            get => _recordedAt;
            set => SetProperty(ref _recordedAt, value);
        }

        private string _temperature = string.Empty;

        public string Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private string _validationMessage = "まだ検証されていません";

        public string ValidationMessage
        {
            get => _validationMessage;
            set => SetProperty(ref _validationMessage, value);
        }

        public MainWindowViewModel()
        {
            ValidateAndRecordCommand =
                new DelegateCommand(ExecuteValidateAndRecord);


            
            ErrorRecordCommand = new DelegateCommand(ExecuteErrorRecord);
            

            LoadRecords();
        }

        private void ExecuteErrorRecord()
        {
            ValidationMessage =
        "ErrorHeatResponseRecord は IValidatable を実装していません。\n" +
        "そのため ValidationToken<T> の型引数として使用できません。";

            var rec = new ErrorHeatResponseRecord();
            // これはコンパイルエラーになる
     //       var token =
      //     ValidationToken<ErrorHeatResponseRecord>.Create(rec);


        }

        private void LoadRecords()
        {
            if (!File.Exists(JsonFilePath))
            {
                return;
            }

            var json = File.ReadAllText(JsonFilePath);

            var records =
      JsonSerializer.Deserialize<List<HeatResponseRecord>>(json);

            if (records is null)
            {
                return;
            }

            Records.Clear();


            foreach (var record in records)
            {
                var validation =
                    ValidationToken<HeatResponseRecord>.Create(record);

                if (validation.Token is null)
                {
                    continue;
                }

                Records.Add(validation.Token);
            }
        }

        public void ValidateAndRecord(HeatResponseRecord record)
        {
            

            var validation = ValidationToken<HeatResponseRecord>.Create(record);

            ValidationMessage = validation!.Result.Message;

            if (validation.Token is null)
            {
                ValidationMessage = "検証失敗";
                return;
            }

            ValidationMessage = "検証成功";

            Records.Add(validation.Token);
        }

        private void ExecuteValidateAndRecord()
        {
            if (!RecordedAt.HasValue)
            {
                ValidationMessage = "記録日時を入力してください。";
                return;
            }

            if (!double.TryParse(Temperature, out var temperature))
            {
                ValidationMessage = "気温を数値で入力してください。";
                return;
            }

            var record = new HeatResponseRecord
            {
                Location = Location,
                RecordedAt = RecordedAt.Value,
                Temperature = temperature
            };

            ValidateAndRecord(record);
        }
    }
}
