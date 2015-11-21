using Microsoft.AspNet.Razor.TagHelpers;
using System;

namespace TagHelperSamples.Bootstrap
{
   [HtmlTargetElement("div", Attributes = ProgressValueAttributeName)]
   public class ProgressBarTagHelper : TagHelper
   {
      private const string ProgressValueAttributeName = "bs-progress-value";
      private const string ProgressMinAttributeName = "bs-progress-min";
      private const string ProgressMaxAttributeName = "bs-progress-max";

      /// <summary>
      /// An expression to be evaluated against the current model.
      /// </summary>
      [HtmlAttributeName(ProgressValueAttributeName)]
      public int ProgressValue { get; set; }

      [HtmlAttributeName(ProgressMinAttributeName)]
      public int ProgressMin { get; set; } = 0;

      [HtmlAttributeName(ProgressMaxAttributeName)]
      public int ProgressMax { get; set; } = 100;

      public override void Process(TagHelperContext context, TagHelperOutput output)
      {
         if (ProgressMin >= ProgressMax)
         {
            throw new ArgumentException($"{ProgressMinAttributeName} must be less than {ProgressMaxAttributeName}");
         }

         if (ProgressValue > ProgressMax || ProgressValue < ProgressMin)
         {
            throw new ArgumentOutOfRangeException(string.Format("{0} must be within the range of {1} and {2}", ProgressValueAttributeName, ProgressMinAttributeName, ProgressMaxAttributeName));
         }
         var progressTotal = ProgressMax - ProgressMin;

         var progressPercentage = Math.Round(((decimal)(ProgressValue - ProgressMin) / (decimal)progressTotal) * 100, 4);

         string progressBarContent =             
$@"<div class='progress-bar' role='progressbar' aria-valuenow='{ProgressValue}' aria-valuemin='{ProgressMin}' aria-valuemax='{ProgressMax}' style='width: {progressPercentage}%;'> 
   <span class='sr-only'>{progressPercentage}% Complete</span>
</div>";

         output.Content.AppendHtml(progressBarContent);

         string classValue;
         if (output.Attributes.ContainsName("class"))
         {
            classValue = string.Format("{0} {1}", output.Attributes["class"], "progress");
         }
         else
         {
            classValue = "progress";
         }

         output.Attributes["class"] = classValue;

         base.Process(context, output);
      }
   }
}
