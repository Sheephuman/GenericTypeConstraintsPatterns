using HeatResponseToken.interfaces;
using HeatResponseToken.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatResponseToken.ViewModel
{
    public class HeatResponseRecord : IValidatable
    {
        public string Location { get; set; } = string.Empty;

        public DateTime RecordedAt { get; set; }

        public double Temperature { get; set; }


        public ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(Location))
            {
                return ValidationResult.Failure("位置情報が入力されていません。");
            }

            if (RecordedAt == default)
            {
                return ValidationResult.Failure("記録日時が入力されていません。");
            }

            if (Temperature < -50 || Temperature > 60)
            {
                return ValidationResult.Failure(
                    "気温は -50℃ ～ 60℃ の範囲で入力してください。");
            }

            return ValidationResult.Success();
        }

    }
}
