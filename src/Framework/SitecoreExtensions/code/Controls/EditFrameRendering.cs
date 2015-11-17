namespace Habitat.Framework.SitecoreExtensions.Controls
{
  using System;
  using System.IO;
  using System.Web.UI;
  using Sitecore.Web.UI.WebControls;

  /// <summary>
  ///   Edit frame class.
  /// </summary>
  /// <remarks>
  ///   This class is required because MVC doesn't support the EditFrame control
  /// </remarks>
  public class EditFrameRendering : IDisposable
  {
    /// <summary>
    ///   The edit frame
    /// </summary>
    private readonly EditFrame editFrame;

    /// <summary>
    ///   The html writer
    /// </summary>
    private readonly HtmlTextWriter htmlWriter;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EditFrameRendering" /> class
    /// </summary>
    /// <param name="writer">The text writer to use</param>
    /// <param name="dataSource">The data source to use</param>
    /// <param name="buttons">The buttons to use</param>
    public EditFrameRendering(TextWriter writer, string dataSource, string buttons)
    {
      this.htmlWriter = new HtmlTextWriter(writer);
      this.editFrame = new EditFrame
      {
        DataSource = dataSource,
        Buttons = buttons
      };
      this.editFrame.RenderFirstPart(this.htmlWriter);
    }

    /// <summary>
    ///   Render the last part of the EditFrame
    /// </summary>
    public void Dispose()
    {
      this.editFrame.RenderLastPart(this.htmlWriter);
      this.htmlWriter.Dispose();
    }
  }
}