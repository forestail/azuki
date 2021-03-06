using System;
using System.Windows.Forms;

namespace Sgry.Ann
{
	static partial class Actions
	{
		/// <summary>
		/// UNDO last operation.
		/// </summary>
		public static AnnAction Undo
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.Undo();
		};

		/// <summary>
		/// Execute again most recently UNDOed operation.
		/// </summary>
		public static AnnAction Redo
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.Redo();
		};

		/// <summary>
		/// Cuts currently selected text or current line if nothing selected.
		/// </summary>
		public static AnnAction Cut
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.Cut();
		};

		/// <summary>
		/// Copies currently selected text or current line if nothing selected.
		/// </summary>
		public static AnnAction Copy
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.Copy();
		};

		/// <summary>
		/// Pastes clipboard text and replace to currently selected text.
		/// </summary>
		public static AnnAction Paste
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.Paste();
		};

		/// <summary>
		/// Shows find dialog.
		/// </summary>
		public static AnnAction ShowFindDialog
			= delegate( AppLogic app )
		{
			app.MainForm.ActivateSearchPanel();
		};

		/// <summary>
		/// Finds next matching pattern.
		/// </summary>
		public static AnnAction FindNext
			= delegate( AppLogic app )
		{
			// set text pattern to emphasize
			app.UpdateWatchPatternForTextSearch();

			// seek to next occurrence
			app.FindNext();
		};

		/// <summary>
		/// Finds previous matching pattern.
		/// </summary>
		public static AnnAction FindPrev
			= delegate( AppLogic app )
		{
			// set text pattern to emphasize
			app.UpdateWatchPatternForTextSearch();

			// seek to previous occurrence
			app.FindPrev();
		};

		/// <summary>
		/// Shows "GotoLine" dialog.
		/// </summary>
		public static AnnAction ShowGotoDialog
			= delegate( AppLogic app )
		{
			var doc = app.ActiveDocument;
			var view = app.MainForm.Azuki.View;
			int initLineNumber;
			bool useScreenLineNumber;
			int indexToGo;

			// Determine initial UI state
			useScreenLineNumber = app.PreviouslyUsedScreenLineNumberInGotoForm;
			if( useScreenLineNumber )
				initLineNumber = view.GetLineIndexFromCharIndex( doc.CaretIndex ) + 1;
			else
				initLineNumber = doc.GetLineIndexFromCharIndex( doc.CaretIndex ) + 1;

			// Show UI and get the destination
			using( var form = new GotoForm() )
			{
				form.LineNumber = initLineNumber;
				form.UseScreenLineNumber = useScreenLineNumber;
				if( form.ShowDialog() != DialogResult.OK )
				{
					return;
				}
				app.PreviouslyUsedScreenLineNumberInGotoForm = form.UseScreenLineNumber;

				if( form.UseScreenLineNumber )
					indexToGo = view.GetLineHeadIndex( Math.Min(form.LineNumber, view.LineCount) - 1 );
				else
					indexToGo = doc.GetLineHeadIndex( Math.Min(form.LineNumber, doc.LineCount) - 1 );
			}

			doc.SetSelection( indexToGo, indexToGo );
			app.MainForm.Azuki.ScrollToCaret();
		};

		/// <summary>
		/// Removes whitespaces at end of each line.
		/// </summary>
		public static AnnAction TrimTrailingSpace
			= delegate( AppLogic app )
		{
			Sgry.Azuki.Actions.TrimTrailingSpace( app.MainForm.Azuki );
		};

		/// <summary>
		/// Removes whitespaces at beginning of each line.
		/// </summary>
		public static AnnAction TrimLeadingSpace
			= delegate( AppLogic app )
		{
			Sgry.Azuki.Actions.TrimLeadingSpace( app.MainForm.Azuki );
		};

		/// <summary>
		/// Convertes tab characters in selection to equivalent amount of
		/// spaces.
		/// </summary>
		public static AnnAction ConvertTabsToSpaces
			= delegate( AppLogic app )
		{
			Sgry.Azuki.Actions.ConvertTabsToSpaces( app.MainForm.Azuki );
		};

		/// <summary>
		/// Convertes space characters in selection to tab characters as much 
		/// as possible.
		/// </summary>
		public static AnnAction ConvertSpacesToTabs
			= delegate( AppLogic app )
		{
			Sgry.Azuki.Actions.ConvertSpacesToTabs( app.MainForm.Azuki );
		};

		/// <summary>
		/// Selects all text.
		/// </summary>
		public static AnnAction SelectAll
			= delegate( AppLogic app )
		{
			app.MainForm.Azuki.SelectAll();
		};

		/// <summary>
		/// Sets EOL code for input to CR+LF
		/// and unify existing EOL code to CR+LF if user choses so.
		/// </summary>
		public static AnnAction SetEolCodeToCRLF
			= delegate( AppLogic app )
		{
			app.SetEolCode( "\r\n" );
		};

		/// <summary>
		/// Sets EOL code for input to LF
		/// and unify existing EOL code to LF if user choses so.
		/// </summary>
		public static AnnAction SetEolCodeToLF
			= delegate( AppLogic app )
		{
			app.SetEolCode( "\n" );
		};

		/// <summary>
		/// Sets EOL code for input to CR
		/// and unify existing EOL code to CR if user choses so.
		/// </summary>
		public static AnnAction SetEolCodeToCR
			= delegate( AppLogic app )
		{
			app.SetEolCode( "\r" );
		};
	}
}
