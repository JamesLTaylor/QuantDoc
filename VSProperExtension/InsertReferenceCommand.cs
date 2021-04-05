using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Task = System.Threading.Tasks.Task;

namespace FirstMenuCommand
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class InsertReferenceCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0101;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("8b5a50cc-a03c-443b-9d32-167f373f1da6");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertReferenceCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private InsertReferenceCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static InsertReferenceCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in InsertReferenceCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new InsertReferenceCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                if (!(await ServiceProvider.GetServiceAsync(typeof(DTE)).ConfigureAwait(false) is DTE2 dte)) return;
                if (!(dte.ActiveWindow.Selection is TextSelection ts)) return;
                var editPoint = ts.ActivePoint.CreateEditPoint();
                var line = editPoint.Line;
                var nextLine = editPoint.GetLines(line + 1, line + 2);
                var count = nextLine.TakeWhile(char.IsWhiteSpace).Count();
                // https://docs.microsoft.com/en-us/dotnet/api/envdte.editpoint?view=visualstudiosdk-2019
                editPoint.Insert(nextLine.Substring(0, count) + "//QD: _math_example_formula");

                var func = ts?.ActivePoint.CodeElement[vsCMElement.vsCMElementFunction];
                var filename = dte.ActiveDocument.FullName;
                var name = func.FullName;
                string message2 = dte.ActiveWindow.Document.FullName + System.Environment.NewLine +
                                  "Line " + ts.CurrentLine + System.Environment.NewLine +
                                  func.FullName;
            }
            catch (Exception)
            {

            }
        }
    }
}
