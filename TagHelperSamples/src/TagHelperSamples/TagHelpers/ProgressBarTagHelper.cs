using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;

namespace TagHelperSamples.TagHelpers
{
   [TargetElement("div", Attributes = ProgressValueAttributeName)]
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
            throw new ArgumentException(string.Format("{0} must be less than {1}", ProgressMinAttributeName, ProgressMaxAttributeName));
         }

         if (ProgressValue > ProgressMax || ProgressValue < ProgressMin)
         {
            throw new ArgumentOutOfRangeException(string.Format("{0} must be within the range of {1} and {2}", ProgressValueAttributeName, ProgressMinAttributeName, ProgressMaxAttributeName));
         }
         var progressTotal = ProgressMax - ProgressMin;

         var progressPercentage = Math.Round(((decimal)(ProgressValue - ProgressMin) / (decimal)progressTotal) * 100, 4);

         string progressBarContent =
             string.Format(
 @"<div class='progress-bar' role='progressbar' aria-valuenow='{0}' aria-valuemin='{1}' aria-valuemax='{2}' style='width: {3}%;'> 
<span class='sr-only'>{3}% Complete</span>
</div>", ProgressValue, ProgressMin, ProgressMax, progressPercentage);

         output.Content.Append(progressBarContent);

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
