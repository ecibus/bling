<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" 
    CodeBehind="ChaseFundingForm.aspx.cs" 
    Inherits="Bling.Web.Compliance.ChaseFundingForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="js/ChaseFundingForm.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<input type="hidden" id="fileId" />

<style>
<!--
 /* Font Definitions */
 @font-face
	{font-family:Helv;
	panose-1:2 11 6 4 2 2 2 3 2 4;
	mso-font-charset:0;
	mso-generic-font-family:swiss;
	mso-font-format:other;
	mso-font-pitch:variable;
	mso-font-signature:3 0 0 0 1 0;}
@font-face
	{font-family:Wingdings;
	panose-1:5 0 0 0 0 0 0 0 0 0;
	mso-font-charset:2;
	mso-generic-font-family:auto;
	mso-font-pitch:variable;
	mso-font-signature:0 268435456 0 0 -2147483648 0;}
@font-face
	{font-family:"Cambria Math";
	panose-1:2 4 5 3 5 4 6 3 2 4;
	mso-font-charset:0;
	mso-generic-font-family:roman;
	mso-font-pitch:variable;
	mso-font-signature:-536870145 1107305727 0 0 415 0;}
@font-face
	{font-family:Calibri;
	panose-1:2 15 5 2 2 2 4 3 2 4;
	mso-font-charset:0;
	mso-generic-font-family:swiss;
	mso-font-pitch:variable;
	mso-font-signature:-536870145 1073786111 1 0 415 0;}
@font-face
	{font-family:Times;
	panose-1:2 2 6 3 5 4 5 2 3 4;
	mso-font-charset:0;
	mso-generic-font-family:roman;
	mso-font-pitch:variable;
	mso-font-signature:-536859905 -1073711039 9 0 511 0;}
@font-face
	{font-family:Tahoma;
	panose-1:2 11 6 4 3 5 4 4 2 4;
	mso-font-charset:0;
	mso-generic-font-family:swiss;
	mso-font-pitch:variable;
	mso-font-signature:-520081665 -1073717157 41 0 66047 0;}
 /* Style Definitions */
 p.MsoNormal, li.MsoNormal, div.MsoNormal
	{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-parent:"";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
h1
	{mso-style-name:"Heading 1\,Part";
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:"Heading 2\,Chapter Title";
	margin-top:0in;
	margin-right:0in;
	margin-bottom:12.0pt;
	margin-left:0in;
	text-align:left;
	mso-pagination:widow-orphan;
	mso-outline-level:1;
	text-autospace:none;
	font-size:16.0pt;
	font-family:"Arial","sans-serif";
	mso-font-kerning:0pt;
	font-weight:bold;}
h2
	{mso-style-name:"Heading 2\,Chapter Title";
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:"Heading 4\,Map Title";
	margin-top:0in;
	margin-right:0in;
	margin-bottom:12.0pt;
	margin-left:0in;
	text-align:left;
	mso-pagination:widow-orphan;
	mso-outline-level:2;
	text-autospace:none;
	font-size:16.0pt;
	font-family:"Arial","sans-serif";
	font-weight:bold;}
h3
	{mso-style-name:"Heading 3\,Section";
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:"Heading 4\,Map Title";
	margin-top:0in;
	margin-right:0in;
	margin-bottom:12.0pt;
	margin-left:0in;
	text-align:center;
	mso-pagination:widow-orphan;
	mso-outline-level:3;
	text-autospace:none;
	font-size:16.0pt;
	font-family:"Arial","sans-serif";
	font-weight:bold;}
h4
	{mso-style-name:"Heading 4\,Map Title";
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:12.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	mso-outline-level:4;
	text-autospace:none;
	font-size:16.0pt;
	font-family:"Arial","sans-serif";
	font-weight:bold;}
h5
	{mso-style-name:"Heading 5\,Block Label";
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	mso-outline-level:5;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	font-weight:bold;}
h6
	{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:3.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	mso-outline-level:6;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	font-weight:normal;
	font-style:italic;}
p.MsoHeading7, li.MsoHeading7, div.MsoHeading7
	{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:3.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	mso-outline-level:7;
	text-autospace:none;
	font-size:10.0pt;
	font-family:"Arial","sans-serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoHeading8, li.MsoHeading8, div.MsoHeading8
	{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:3.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	mso-outline-level:8;
	text-autospace:none;
	font-size:10.0pt;
	font-family:"Arial","sans-serif";
	mso-fareast-font-family:"Times New Roman";
	font-style:italic;}
p.MsoHeading9, li.MsoHeading9, div.MsoHeading9
	{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:3.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	mso-outline-level:9;
	text-autospace:none;
	font-size:9.0pt;
	font-family:"Arial","sans-serif";
	mso-fareast-font-family:"Times New Roman";
	font-weight:bold;
	font-style:italic;}
p.MsoCommentText, li.MsoCommentText, div.MsoCommentText
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	mso-style-link:"Comment Text Char";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoHeader, li.MsoHeader, div.MsoHeader
	{mso-style-priority:99;
	mso-style-unhide:no;
	mso-style-link:"Header Char";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	tab-stops:center 3.0in right 6.0in;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoFooter, li.MsoFooter, div.MsoFooter
	{mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	tab-stops:center 3.0in right 6.0in;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
span.MsoCommentReference
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	mso-style-parent:"";
	mso-ansi-font-size:8.0pt;
	mso-bidi-font-size:8.0pt;}
p.MsoMacroText, li.MsoMacroText, div.MsoMacroText
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	mso-style-parent:"";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	tab-stops:24.0pt 48.0pt 1.0in 96.0pt 120.0pt 2.0in 168.0pt 192.0pt 3.0in;
	text-autospace:none;
	font-size:10.0pt;
	font-family:"Courier New";
	mso-fareast-font-family:"Times New Roman";}
p.MsoBodyText, li.MsoBodyText, div.MsoBodyText
	{mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	text-align:center;
	line-height:12.0pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Helv","sans-serif";
	mso-fareast-font-family:"Times New Roman";
	mso-bidi-font-family:"Times New Roman";
	color:black;}
p.MsoBlockText, li.MsoBlockText, div.MsoBlockText
	{mso-style-name:"Block Text\,Block Text Char\,Block Text Char2 Char\,Block Text Char1 Char Char\,Block Text Char2 Char Char Char\,Block Text Char1 Char Char Char Char\,Block Text Char Char Char Char Char Char\,Block Text Char1 Char Char Char Char Char Char\,Block Text Char1";
	mso-style-unhide:no;
	mso-style-link:"Block Text Char2\,Block Text Char Char\,Block Text Char2 Char Char\,Block Text Char1 Char Char Char\,Block Text Char2 Char Char Char Char\,Block Text Char1 Char Char Char Char Char\,Block Text Char Char Char Char Char Char Char\,Block Text Char1 Char";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
a:link, span.MsoHyperlink
	{mso-style-unhide:no;
	mso-style-parent:"";
	color:blue;
	text-decoration:underline;
	text-underline:single;}
a:visited, span.MsoHyperlinkFollowed
	{mso-style-unhide:no;
	color:#954F72;
	mso-themecolor:followedhyperlink;
	text-decoration:underline;
	text-underline:single;}
p.MsoDocumentMap, li.MsoDocumentMap, div.MsoDocumentMap
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	background:navy;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Tahoma","sans-serif";
	mso-fareast-font-family:"Times New Roman";}
p
	{mso-style-priority:99;
	mso-margin-top-alt:auto;
	margin-right:0in;
	mso-margin-bottom-alt:auto;
	margin-left:0in;
	mso-pagination:widow-orphan;
	font-size:10.0pt;
	font-family:"Arial","sans-serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoCommentSubject, li.MsoCommentSubject, div.MsoCommentSubject
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	mso-style-parent:"Comment Text";
	mso-style-next:"Comment Text";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";
	font-weight:bold;}
p.MsoAcetate, li.MsoAcetate, div.MsoAcetate
	{mso-style-noshow:yes;
	mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:8.0pt;
	font-family:"Tahoma","sans-serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoListParagraph, li.MsoListParagraph, div.MsoListParagraph
	{mso-style-priority:34;
	mso-style-unhide:no;
	mso-style-qformat:yes;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	mso-add-space:auto;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoListParagraphCxSpFirst, li.MsoListParagraphCxSpFirst, div.MsoListParagraphCxSpFirst
	{mso-style-priority:34;
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-type:export-only;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	mso-add-space:auto;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoListParagraphCxSpMiddle, li.MsoListParagraphCxSpMiddle, div.MsoListParagraphCxSpMiddle
	{mso-style-priority:34;
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-type:export-only;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	mso-add-space:auto;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MsoListParagraphCxSpLast, li.MsoListParagraphCxSpLast, div.MsoListParagraphCxSpLast
	{mso-style-priority:34;
	mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-type:export-only;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	mso-add-space:auto;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.BlockLine, li.BlockLine, div.BlockLine
	{mso-style-name:"Block Line";
	mso-style-unhide:no;
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:85.0pt;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	border:none;
	mso-border-top-alt:solid windowtext .75pt;
	padding:0in;
	mso-padding-alt:1.0pt 0in 0in 0in;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.BulletText1, li.BulletText1, div.BulletText1
	{mso-style-name:"Bullet Text 1";
	mso-style-unhide:no;
	mso-style-link:"Bullet Text 1 Char";
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.25in;
	margin-bottom:.0001pt;
	text-indent:-.25in;
	mso-pagination:widow-orphan;
	mso-list:l5 level1 lfo2;
	tab-stops:list .25in;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.BulletText2, li.BulletText2, div.BulletText2
	{mso-style-name:"Bullet Text 2";
	mso-style-update:auto;
	mso-style-unhide:no;
	mso-style-parent:"Bullet Text 1";
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	text-indent:-.25in;
	mso-pagination:widow-orphan;
	mso-list:l1 level1 lfo3;
	tab-stops:decimal .25in;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.ContinuedOnNextPa, li.ContinuedOnNextPa, div.ContinuedOnNextPa
	{mso-style-name:"Continued On Next Pa";
	mso-style-unhide:no;
	mso-style-next:Normal;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:85.0pt;
	margin-bottom:.0001pt;
	text-align:right;
	mso-pagination:widow-orphan;
	text-autospace:none;
	border:none;
	mso-border-top-alt:solid windowtext .75pt;
	padding:0in;
	mso-padding-alt:1.0pt 0in 0in 0in;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";
	font-style:italic;}
p.ContinuedTableLabe, li.ContinuedTableLabe, div.ContinuedTableLabe
	{mso-style-name:"Continued Table Labe";
	mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.MapTitleContinued, li.MapTitleContinued, div.MapTitleContinued
	{mso-style-name:"Map Title\. Continued";
	mso-style-unhide:no;
	margin-top:0in;
	margin-right:0in;
	margin-bottom:12.0pt;
	margin-left:0in;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:16.0pt;
	font-family:"Arial","sans-serif";
	mso-fareast-font-family:"Times New Roman";
	font-weight:bold;}
p.MemoLine, li.MemoLine, div.MemoLine
	{mso-style-name:"Memo Line";
	mso-style-unhide:no;
	mso-style-parent:"Block Line";
	mso-style-next:Normal;
	margin-top:12.0pt;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	border:none;
	mso-border-top-alt:solid windowtext .75pt;
	padding:0in;
	mso-padding-alt:1.0pt 0in 0in 0in;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.TableText, li.TableText, div.TableText
	{mso-style-name:"Table Text";
	mso-style-unhide:no;
	mso-style-link:"Table Text Char";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.NoteText, li.NoteText, div.NoteText
	{mso-style-name:"Note Text";
	mso-style-unhide:no;
	mso-style-parent:"Block Text\,Block Text Char\,Block Text Char2 Char\,Block Text Char1 Char Char\,Block Text Char2 Char Char Char\,Block Text Char1 Char Char Char Char\,Block Text Char Char Char Char Char Char\,Block Text Char1 Char Char Char Char Char Char\,Block Text Char1";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
p.TableHeaderText, li.TableHeaderText, div.TableHeaderText
	{mso-style-name:"Table Header Text";
	mso-style-unhide:no;
	mso-style-parent:"Table Text";
	margin:0in;
	margin-bottom:.0001pt;
	text-align:center;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";
	font-weight:bold;}
p.EmbeddedText, li.EmbeddedText, div.EmbeddedText
	{mso-style-name:"Embedded Text";
	mso-style-unhide:no;
	mso-style-parent:"Table Text";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	text-autospace:none;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";}
span.BulletText1Char
	{mso-style-name:"Bullet Text 1 Char";
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-parent:"";
	mso-style-link:"Bullet Text 1";
	mso-ansi-font-size:12.0pt;
	mso-bidi-font-size:12.0pt;}
span.BlockTextChar2
	{mso-style-name:"Block Text Char2\,Block Text Char Char\,Block Text Char2 Char Char\,Block Text Char1 Char Char Char\,Block Text Char2 Char Char Char Char\,Block Text Char1 Char Char Char Char Char\,Block Text Char Char Char Char Char Char Char\,Block Text Char1 Char";
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-parent:"";
	mso-style-link:"Block Text\,Block Text Char\,Block Text Char2 Char\,Block Text Char1 Char Char\,Block Text Char2 Char Char Char\,Block Text Char1 Char Char Char Char\,Block Text Char Char Char Char Char Char\,Block Text Char1 Char Char Char Char Char Char\,Block Text Char1";
	mso-ansi-font-size:12.0pt;
	mso-bidi-font-size:12.0pt;
	mso-ansi-language:EN-US;
	mso-fareast-language:EN-US;
	mso-bidi-language:AR-SA;}
p.ContinuedBlockLabel, li.ContinuedBlockLabel, div.ContinuedBlockLabel
	{mso-style-name:"Continued Block Label";
	mso-style-unhide:no;
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	font-size:11.0pt;
	mso-bidi-font-size:10.0pt;
	font-family:"Times New Roman","serif";
	mso-fareast-font-family:"Times New Roman";
	font-weight:bold;
	mso-bidi-font-weight:normal;}
span.TableTextChar
	{mso-style-name:"Table Text Char";
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-parent:"";
	mso-style-link:"Table Text";
	mso-ansi-font-size:11.0pt;
	mso-bidi-font-size:11.0pt;
	mso-ansi-language:EN-US;
	mso-fareast-language:EN-US;
	mso-bidi-language:AR-SA;}
span.HeaderChar
	{mso-style-name:"Header Char";
	mso-style-priority:99;
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-parent:"";
	mso-style-link:Header;
	mso-ansi-font-size:12.0pt;
	mso-bidi-font-size:12.0pt;}
span.CommentTextChar
	{mso-style-name:"Comment Text Char";
	mso-style-noshow:yes;
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-link:"Comment Text";}
.MsoChpDefault
	{mso-style-type:export-only;
	mso-default-props:yes;
	font-size:10.0pt;
	mso-ansi-font-size:10.0pt;
	mso-bidi-font-size:10.0pt;}
 /* Page Definitions */
 @page
	{mso-footnote-separator:url("Chase%20funding%20form_files/header.htm") fs;
	mso-footnote-continuation-separator:url("Chase%20funding%20form_files/header.htm") fcs;
	mso-endnote-separator:url("Chase%20funding%20form_files/header.htm") es;
	mso-endnote-continuation-separator:url("Chase%20funding%20form_files/header.htm") ecs;}
@page WordSection1
	{size:8.5in 14.0in;
	margin:.5in .5in .5in .5in;
	mso-header-margin:.4in;
	mso-footer-margin:.2in;
	mso-page-numbers:1;
	mso-header:url("Chase%20funding%20form_files/header.htm") h1;
	mso-footer:url("Chase%20funding%20form_files/header.htm") f1;
	mso-first-header:url("Chase%20funding%20form_files/header.htm") fh1;
	mso-first-footer:url("Chase%20funding%20form_files/header.htm") ff1;
	mso-paper-source:0;}
div.WordSection1
	{page:WordSection1;}
 /* List Definitions */
 @list l0
	{mso-list-id:511065577;
	mso-list-type:hybrid;
	mso-list-template-ids:149331040 2075551938 67698713 67698715 67698703 67698713 67698715 67698703 67698713 67698715;}
@list l0:level1
	{mso-level-number-format:alpha-lower;
	mso-level-text:"1%1\.";
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:.75in;
	text-indent:-.25in;}
@list l0:level2
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:1.25in;
	text-indent:-.25in;}
@list l0:level3
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:1.75in;
	text-indent:-9.0pt;}
@list l0:level4
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:2.25in;
	text-indent:-.25in;}
@list l0:level5
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:2.75in;
	text-indent:-.25in;}
@list l0:level6
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:3.25in;
	text-indent:-9.0pt;}
@list l0:level7
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:3.75in;
	text-indent:-.25in;}
@list l0:level8
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:4.25in;
	text-indent:-.25in;}
@list l0:level9
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:4.75in;
	text-indent:-9.0pt;}
@list l1
	{mso-list-id:707530065;
	mso-list-type:simple;
	mso-list-template-ids:-312465442;}
@list l1:level1
	{mso-level-number-format:bullet;
	mso-level-style-link:"Bullet Text 2";
	mso-level-text:\F0B7;
	mso-level-tab-stop:.25in;
	mso-level-number-position:left;
	margin-left:.25in;
	text-indent:-.25in;
	font-family:Symbol;
	mso-bidi-font-family:"Times New Roman";
	color:windowtext;}
@list l2
	{mso-list-id:802384500;
	mso-list-type:hybrid;
	mso-list-template-ids:1809609774 1560458300 67698713 67698715 67698703 67698713 67698715 67698703 67698713 67698715;}
@list l2:level1
	{mso-level-number-format:alpha-lower;
	mso-level-text:"10%1\.";
	mso-level-tab-stop:none;
	mso-level-number-position:center;
	margin-left:.75in;
	text-indent:-.25in;}
@list l2:level2
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:1.25in;
	text-indent:-.25in;}
@list l2:level3
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:1.75in;
	text-indent:-9.0pt;}
@list l2:level4
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:2.25in;
	text-indent:-.25in;}
@list l2:level5
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:2.75in;
	text-indent:-.25in;}
@list l2:level6
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:3.25in;
	text-indent:-9.0pt;}
@list l2:level7
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:3.75in;
	text-indent:-.25in;}
@list l2:level8
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:4.25in;
	text-indent:-.25in;}
@list l2:level9
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:4.75in;
	text-indent:-9.0pt;}
@list l3
	{mso-list-id:812988149;
	mso-list-type:hybrid;
	mso-list-template-ids:-2046279722 67698689 67698691 67698693 67698689 67698691 67698693 67698689 67698691 67698693;}
@list l3:level1
	{mso-level-number-format:bullet;
	mso-level-text:\F0B7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Symbol;}
@list l3:level2
	{mso-level-number-format:bullet;
	mso-level-text:o;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:"Courier New";}
@list l3:level3
	{mso-level-number-format:bullet;
	mso-level-text:\F0A7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Wingdings;}
@list l3:level4
	{mso-level-number-format:bullet;
	mso-level-text:\F0B7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Symbol;}
@list l3:level5
	{mso-level-number-format:bullet;
	mso-level-text:o;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:"Courier New";}
@list l3:level6
	{mso-level-number-format:bullet;
	mso-level-text:\F0A7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Wingdings;}
@list l3:level7
	{mso-level-number-format:bullet;
	mso-level-text:\F0B7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Symbol;}
@list l3:level8
	{mso-level-number-format:bullet;
	mso-level-text:o;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:"Courier New";}
@list l3:level9
	{mso-level-number-format:bullet;
	mso-level-text:\F0A7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Wingdings;}
@list l4
	{mso-list-id:1096947938;
	mso-list-type:hybrid;
	mso-list-template-ids:-1882444074 -976977598 -51377214 67698715 67698703 67698713 67698715 67698703 67698713 67698715;}
@list l4:level1
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:.25in;
	text-indent:-.25in;
	mso-ansi-font-style:normal;}
@list l4:level2
	{mso-level-number-format:alpha-lower;
	mso-level-text:"10%2\.";
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:.75in;
	text-indent:-.25in;}
@list l4:level3
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:1.25in;
	text-indent:-9.0pt;}
@list l4:level4
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:1.75in;
	text-indent:-.25in;}
@list l4:level5
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:2.25in;
	text-indent:-.25in;}
@list l4:level6
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:2.75in;
	text-indent:-9.0pt;}
@list l4:level7
	{mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:3.25in;
	text-indent:-.25in;}
@list l4:level8
	{mso-level-number-format:alpha-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	margin-left:3.75in;
	text-indent:-.25in;}
@list l4:level9
	{mso-level-number-format:roman-lower;
	mso-level-tab-stop:none;
	mso-level-number-position:right;
	margin-left:4.25in;
	text-indent:-9.0pt;}
@list l5
	{mso-list-id:1598560430;
	mso-list-type:simple;
	mso-list-template-ids:-1046584308;}
@list l5:level1
	{mso-level-number-format:bullet;
	mso-level-style-link:"Bullet Text 1";
	mso-level-text:\F0B7;
	mso-level-tab-stop:.25in;
	mso-level-number-position:left;
	margin-left:.25in;
	text-indent:-.25in;
	font-family:Symbol;
	mso-bidi-font-family:"Times New Roman";}
@list l6
	{mso-list-id:1909798582;
	mso-list-type:hybrid;
	mso-list-template-ids:1533081642 67698689 67698691 67698693 67698689 67698691 67698693 67698689 67698691 67698693;}
@list l6:level1
	{mso-level-number-format:bullet;
	mso-level-text:\F0B7;
	mso-level-tab-stop:none;
	mso-level-number-position:left;
	text-indent:-.25in;
	font-family:Symbol;}
@list l6:level2
	{mso-level-tab-stop:1.0in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level3
	{mso-level-tab-stop:1.5in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level4
	{mso-level-tab-stop:2.0in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level5
	{mso-level-tab-stop:2.5in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level6
	{mso-level-tab-stop:3.0in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level7
	{mso-level-tab-stop:3.5in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level8
	{mso-level-tab-stop:4.0in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level9
	{mso-level-tab-stop:4.5in;
	mso-level-number-position:left;
	text-indent:-.25in;}
@list l6:level1 lfo4
	{mso-level-start-at:0;}
ol
	{margin-bottom:0in;}
ul
	{margin-bottom:0in;}
-->
</style>

<%--<body lang=EN-US link=blue vlink="#954F72" style='tab-interval:.5in;text-justify-trim:
punctuation'>--%>

<h2>Chase Funding Form</h2>
Loan Number : <input id="txtLoanNumber" type="text" value="" />
<input id="btnLoad" type="button" value="Load" />
<input id="btnPrintPreview" type="button" value="Print Preview" disabled="disabled"/>

<br />
<br />

<div class=WordSection1>

<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:28.75pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
  height:28.75pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif";
  color:white'>Complete the information below and submit with all closed loan
  packages.<span style='mso-spacerun:yes'>  </span><o:p></o:p></span></b></p>
  <p class=MsoNormal><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
  color:white'>See last page for delivery instructions.<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:.3in'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Correspondent
  Name:<o:p></o:p></span></b></p>
  </td>
  <td width=185 style='width:138.45pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes' id="correspondentName">
    
    <%--<input type="text" />--%>
  </span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>

  
  </td>
  <td width=171 colspan=2 style='width:128.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal align=right style='text-align:right;mso-outline-level:
  1'><b style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>C-Code:<o:p></o:p></span></b></p>
  </td>
  <td width=167 style='width:125.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span id="ccode"
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:.3in'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Delivery
  Contact Name:<o:p></o:p></span></b></p>
  </td>
  <td width=185 style='width:138.45pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span id="deliveryContactName"
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
  <td width=171 colspan=2 style='width:128.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal align=right style='text-align:right;mso-outline-level:
  1'><b style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>Contact Phone #:<o:p></o:p></span></b></p>
  </td>
  <td width=167 style='width:125.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span id="contactPhone"
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:.3in'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Correspondent
  Underwriting Contact Name:<o:p></o:p></span></b></p>
  </td>
  <td width=185 style='width:138.45pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  <span id='underwriter' style='mso-no-proof:yes'>&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
  <td width=171 colspan=2 style='width:128.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal align=right style='text-align:right;mso-outline-level:
  1'><b style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>Underwriting Contact Phone #:<o:p></o:p></span></b></p>
  </td>
  <td width=167 style='width:125.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  <span id="underwritingPhoneNo" style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:.3in'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Chase
  Loan #:<o:p></o:p></span></b></p>
  </td>
  <td width=185 style='width:138.45pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  <span id="investorLoanNumber" style='mso-no-proof:yes'>&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  &nbsp;
  </td>
  <td width=171 colspan=2 style='width:128.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal align=right style='text-align:right;mso-outline-level:
  1'><b style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>Contact Fax #:<o:p></o:p></span></b></p>
  </td>
  <td width=167 style='width:125.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span id="contactFax"
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;height:.3in'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Correspondent
  Loan #:<o:p></o:p></span></b></p>
  </td>
  <td width=185 style='width:138.45pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span id="loanNumber"
  style='mso-no-proof:yes'>&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
  <td width=171 colspan=2 style='width:128.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal align=right style='text-align:right;mso-outline-level:
  1'><b style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>Commitment #:<o:p></o:p></span></b></p>
  </td>
  <td width=167 style='width:125.2pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  <span id="commitmentNo" style='mso-no-proof:yes'>&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:6;height:25.6pt'>
  <td width=211 style='width:158.6pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:25.6pt'>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Product
  Description:<o:p></o:p></span></b></p>
  <p class=MsoNormal style='mso-outline-level:1'><b style='mso-bidi-font-weight:
  normal'><span style='font-size:8.0pt;mso-bidi-font-size:11.0pt;font-family:
  "Arial","sans-serif"'>(not required on Mandatory loans)</span></b><b
  style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'><o:p></o:p></span></b></p>
  </td>
  <td width=523 colspan=4 style='width:392.2pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:25.6pt'>
  <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  <span id="productDescription"
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>

 </table>
 <br /><br />
 <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>

 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:10.75pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
  height:10.75pt'>

<%-- <tr style='mso-yfti-irow:7;height:10.75pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#006EC7;padding:0in 5.4pt 0in 5.4pt;height:10.75pt'>
--%>  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Part A:
  Regulatory Required Data<o:p></o:p></span></b></p>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Must be
  completed on ALL loans and included in the closed loan package</span></b><b
  style='mso-bidi-font-weight:normal'><span style='font-size:10.0pt;font-family:
  "Arial","sans-serif";color:white'><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:8;height:17.3pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>General
  Information<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:9;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>1.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Application Date (as defined by the date you, the
  Correspondent, took the loan application based on regulatory requirements and
  internal policies and procedures for GFE disclosure)<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes' id="applicationDate"></span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:10;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.75in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l0 level1 lfo8'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>1a.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Purchase Property –
  If the initial GFE was not issued within 3 days of the application date, as
  the property was not yet located, please list the date the property was identified<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes'>&nbsp;<input type="text" id="txtPurchaseProperty"/></span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:11;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>2.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Last date you (Correspondent) locked the loan with the
  borrower (not with Chase)<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes'>&nbsp;<input type="text" id="txtItem2" /></span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:12;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>3.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>If the loan is an ARM, list the initial ARM index value
  and margin used to underwrite the transaction<o:p></o:p></span></p>
  <p class=MsoNormal style='mso-outline-level:1'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoNormal style='margin-left:.25in;mso-outline-level:1'><b
  style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Note</span></b><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'>: <i style='mso-bidi-font-style:normal'>The
  ARM index must have been effective within 45 days of closing in order to be
  valid</i><o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=156 valign=top style='width:117.1pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Index<o:p></o:p></span></p>
    </td>
    <td width=167 valign=top style='width:125.35pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes' id='armIndex'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF00000000140000000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>
    <td width=156 valign=top style='width:117.1pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Margin<o:p></o:p></span></p>
    </td>
    <td width=167 valign=top style='width:125.35pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes' id='armMargin'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF00000000140000000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>4.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>If Occupancy is Investment, does the borrower intend to
  occupy the property for more than 14 days in a year?<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'> <input type="checkbox" id="chkOccInvestmentYes" /> 
  </span></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No <span
  style='mso-spacerun:yes'><input type="checkbox" id="chkOccInvestmentNo" />    </span></span><!--[if supportFields]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Not Applicable<o:p></o:p><input type="checkbox" id="chkOccInvestmentNA" /></span></p>
  </td>
 </tr>

  <tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=5 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
    <input type="button" id="btnSaveGeneralInformation" value="Save Item 1a, 2 and 4" />
  </td>
 </tr>



 <tr style='mso-yfti-irow:14;height:17.3pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>ATR-QM,
  Appraisal Standards and Escrows<o:p></o:p></span></b></p>
  </td>
 </tr>


 <tr style='mso-yfti-irow:15;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>5.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>ATR-QM Designation<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'><input type="checkbox" id="chkQMSafeHarbor"/>  </span>QM-Safe Harbor<o:p></o:p></span></p>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'><input type="checkbox" id="chkQMRebuttablePresumption" />  </span>QM – Rebuttable Presumption<o:p></o:p></span></p>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'><input type="checkbox" id="chkNonQM" />  </span>Non-QM<o:p></o:p></span></p>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'><input type="checkbox" id="chkQMNotApplicable" />  </span>Not Applicable (aka investment for business
  purposes)<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:16;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>6.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>APOR calculated percentage <o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes'><input type="text" id="txtAPORPcnt" style="width:5em" /></span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:12.0pt;font-family:"Arial","sans-serif"'>
  
  %</span><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:17;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>7.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Is the loan considered HPML under QM definition <i
  style='mso-bidi-font-style:normal'>(APR exceeds APOR by more than 1.5%)</i><o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'></span><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span><%--Yes<span style='mso-spacerun:yes'>  
  
  </span><input type="checkbox" disabled="disabled" id="IsHPMLQMYes" /> </span><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'></span><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'> 
  </span>No<o:p></o:p><input type="checkbox" disabled="disabled" id="IsHPMLQMNo" /></span>--%></p>
  </td>
 </tr>

 
 <tr style='mso-yfti-irow:22;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.75in;text-indent:-.75in;mso-text-indent-alt:
  -.25in;mso-outline-level:1;mso-list:l2 level1 lfo9'><![if !supportLists]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
  Arial'><span style='mso-list:Ignore'><span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span>7a.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Conventional or VA Loan: APR exceeds APOR by more than 1.5%<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'>
  
  <input type="checkbox" id="Item7AYes" />
  
  <!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No</span>
  
  <input type="checkbox" id="Item7ANo" />
  
  </p>
  </td>
 </tr>

 
 <tr style='mso-yfti-irow:22;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.75in;text-indent:-.75in;mso-text-indent-alt:
  -.25in;mso-outline-level:1;mso-list:l2 level1 lfo9'><![if !supportLists]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
  Arial'><span style='mso-list:Ignore'><span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span>7b.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>FHA Loan: APR that exceeds the APOR as of the date the interest rate is set plus 115 bps, plus the annual ongoing MIP<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'>
  
  <input type="checkbox" id="Item7BYes" />
  
  <!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No</span>
    <input type="checkbox" id="Item7BNo" />
  
  </p>
  </td>
 </tr>



 <tr style='mso-yfti-irow:18;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>8.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Is the loan considered HPML under Appraisal and Escrow definition
  (Conforming Loans: APR exceeds APOR by more than 1.5%<i style='mso-bidi-font-style:
  normal'>. </i>Jumbo Loans: APR exceeds APOR by more than 2.5%)<i
  style='mso-bidi-font-style:normal'><o:p></o:p></i></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span><input type="checkbox" id="Item8Yes" /></span></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  &nbsp;</span>No<o:p></o:p><input type="checkbox" id="Item8No" /></span></span></p>
  </td>
 </tr>

 <tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=5 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
    <input type="button" value="Save Item 5, 6, 7A, 7B and 8" id="btnSaveATRQM" />
  </td>

 </tr>


 <tr style='mso-yfti-irow:19;height:17.3pt'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>High
  Cost and Various State Level Test Requirements<o:p></o:p></span></b></p>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif";
  color:white'>Note: Chase will not purchase any loan which does not pass the
  federal/state tests.<span style='mso-spacerun:yes'>  </span>Cures are not
  permitted<b style='mso-bidi-font-weight:normal'><o:p></o:p></b></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:20;height:.3in;mso-prop-change:"Kelly Camp" 20131031T1148'
  style='mso-yfti-irow:20 !msorm;height:.3in !msorm'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;border:solid windowtext 1.0pt !msorm;border-top:
  none !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;mso-border-alt:
  solid windowtext .5pt !msorm;padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>9.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif"'>Copy of test, including but not limited to: QM Points
  and Fees, HOEPA, all State and Municipality tests must be included in the
  loan file <o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;border-top:
  none !msorm;border-left:none !msorm;border-bottom:solid windowtext 1.0pt !msorm;
  border-right:solid windowtext 1.0pt !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;
  mso-border-left-alt:solid windowtext .5pt !msorm;mso-border-alt:solid windowtext .5pt !msorm;
  padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'>Include all
  specific details showing how the loan was tested (i.e. fees, credits, APR,
  APOR, etc)<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:21;height:.3in;mso-prop-change:"Kelly Camp" 20131031T1148'
  style='mso-yfti-irow:21 !msorm;height:.3in !msorm'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;border:solid windowtext 1.0pt !msorm;border-top:
  none !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;mso-border-alt:
  solid windowtext .5pt !msorm;padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>10.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>If the transaction
  is a Refinance, was it a Same Lender Refinance<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;border-top:
  none !msorm;border-left:none !msorm;border-bottom:solid windowtext 1.0pt !msorm;
  border-right:solid windowtext 1.0pt !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;
  mso-border-left-alt:solid windowtext .5pt !msorm;mso-border-alt:solid windowtext .5pt !msorm;
  padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'>
  <input type="checkbox" disabled="disabled" />
  <!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No<o:p></o:p>
  
  <input type="checkbox" disabled="disabled" id="chkSameLenderNo" />
  </span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:22;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.75in;text-indent:-.75in;mso-text-indent-alt:
  -.25in;mso-outline-level:1;mso-list:l2 level1 lfo9'><![if !supportLists]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
  Arial'><span style='mso-list:Ignore'><span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span>10a.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>If Yes, was the
  loan being paid off subject to a prepayment penalty?<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'>
  
  <input type="checkbox" disabled="disabled" />
  
  <!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No</span>
  
  <input type="checkbox" disabled="disabled" />
  
  </p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:23;height:.3in'>
  <td width=445 colspan=3 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.75in;text-indent:-.75in;mso-text-indent-alt:
  -.25in;mso-outline-level:1;mso-list:l2 level1 lfo9'><![if !supportLists]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
  Arial'><span style='mso-list:Ignore'><span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span>10b.<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>If loan being paid
  off was<span style='mso-spacerun:yes'>  </span>subject to a prepayment
  penalty, list amount:<o:p></o:p></span></p>
  </td>
  <td width=289 colspan=2 style='width:216.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText style='margin-left:-.05pt'><span style='font-family:"Arial","sans-serif"'>$<span
  style='mso-spacerun:yes'>  
  
  <input type="text" id="txtPrepaymentPenaltyAmount" disabled="disabled" style="width:7em" /> 
  
  </span></span><!--[if supportFields]><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--></p>
  </td>
 </tr>

  <%--<tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=5 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
    <input type="button" value="Save Item 10b" id="btnSaveHighCost" />
  </td>

 </tr>--%>



 <tr style='mso-yfti-irow:24;mso-yfti-lastrow:yes;height:.3in'>
  <td width=734 colspan=5 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:10.0pt;mso-no-proof:yes'>Notes</span></b><span
  style='font-size:10.0pt;mso-no-proof:yes'>: <o:p></o:p></span></p>
  <ul style='margin-top:0in' type=disc>
   <li class=MsoNormal style='mso-list:l3 level1 lfo6'><span style='font-size:
       10.0pt;mso-no-proof:yes'>The dates and rate information required above
       are not based on when the file was registered, delivered or locked with
       Chase.<span style='mso-spacerun:yes'>  </span>This information is based
       on the same dates you (Correspondent) have used for the various regulatory
       requirements.<o:p></o:p></span></li>
  </ul>
  <p class=TableText style='margin-left:.5in;text-indent:-.25in;mso-list:l3 level1 lfo6'><![if !supportLists]><span
  style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
  Symbol'><span style='mso-list:Ignore'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:10.0pt'>The information
  contained in this form is not to be construed as legal or compliance advice,
  and is not meant to be used as a summary of the laws.<span
  style='mso-spacerun:yes'>  </span>If you have any questions related to this
  or any other law, you are strongly encouraged to contact your Legal and
  Compliance Counsel for further guidance.</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <![if !supportMisalignedColumns]>
 <tr height=0>
  <td width=205 style='border:none'></td>
  <td width=175 style='border:none'></td>
  <td width=49 style='border:none'></td>
  <td width=119 style='border:none'></td>
  <td width=159 style='border:none'></td>
 </tr>
 <![endif]>
</table>

<span style='font-size:12.0pt;font-family:"Times New Roman","serif";mso-fareast-font-family:
"Times New Roman";mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA'><br clear=all style='mso-special-character:line-break;
page-break-before:always'>
</span>

<p class=MsoNormal><span style='font-size:10.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>

<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:10.75pt'>
  <td width=734 colspan=2 style='width:7.65in;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
  height:10.75pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Part A:
  Regulatory Data Continued<o:p></o:p></span></b></p>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Must be
  completed on ALL loans and included in the closed loan package</span></b><b
  style='mso-bidi-font-weight:normal'><span style='font-size:10.0pt;font-family:
  "Arial","sans-serif";color:white'><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:17.3pt'>
  <td width=734 colspan=2 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>High
  Cost and Various State Level Test Requirements, continued<o:p></o:p></span></b></p>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif";
  color:white'>Note: Chase will not purchase any loan which does not pass the
  federal/state tests.<span style='mso-spacerun:yes'>  </span>Cures are not
  permitted<b style='mso-bidi-font-weight:normal'><o:p></o:p></b></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:.3in'>
  <td width=373 style='width:279.65pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='mso-outline-level:1'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>11.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>HOEPA APR<i
  style='mso-bidi-font-style:normal'><o:p></o:p></i></span></p>
  </td>
  <td width=362 style='width:271.15pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText style='margin-left:3.85pt'><!--[if supportFields]><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-no-proof:yes'><input type="text" style="width:3em" id="hoepaAPR" /></span><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-size:10.0pt;
  mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
  style='mso-element:field-end'></span></span><![endif]--><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
  %</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:.3in'>
  <td width=373 style='width:279.65pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>12.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Were any bonafide
  discount points excluded from the Points and Fee test (QM, HOEPA or State
  level)? <o:p></o:p></span></p>
  </td>
  <td width=362 style='width:271.15pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText style='margin-left:3.85pt'><!--[if supportFields]><span
  style='font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span>

  <input type="radio" name="rdoPointsExcluded" value="1" id="rdoPointsExcludedYes"/>
  
  </span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No<o:p></o:p>

  <input type="radio" name="rdoPointsExcluded" value="0" id="rdoPointsExcludedNo"/>
  
  </span></p>
  <p class=TableText style='margin-left:3.85pt'><span style='font-family:"Arial","sans-serif"'>If
  yes, you must complete the Bonafide Discount point portion of this form<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:.3in'>
  <td width=373 style='width:279.65pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>13.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Were any bonafide
  fees imposed by a third party in which no portion was retained by the
  creditor, loan originator or affiliate of either excluded from the Points and
  Fees test?<o:p></o:p></span></p>
  </td>
  <td width=362 style='width:271.15pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText style='margin-left:3.85pt'><!--[if supportFields]><span
  style='font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>Yes<span style='mso-spacerun:yes'>  
  </span>
  
  <input type="radio" name="rdoFeesImposed" value="1" id="chkFeesImposedYes"/>

  </span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'>  </span>No<o:p></o:p>
  
  <input type="radio" name="rdoFeesImposed" value="0" id="rdoFeesImposedNo"/>
  
  </span></p>
  <p class=TableText style='margin-left:3.85pt'><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;height:.3in;mso-prop-change:"Kelly Camp" 20131031T1148'
  style='mso-yfti-irow:5 !msorm;height:.3in !msorm'>
  <td width=373 style='width:279.65pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;border:solid windowtext 1.0pt !msorm;border-top:
  none !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;mso-border-alt:
  solid windowtext .5pt !msorm;padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>14.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>If the property is
  in the state of NY, is a primary residence and meets the loan limit required
  to be tested, please provide the following:<o:p></o:p></span></p>
  </td>
  <td width=362 style='width:271.15pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;border-top:
  none !msorm;border-left:none !msorm;border-bottom:solid windowtext 1.0pt !msorm;
  border-right:solid windowtext 1.0pt !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;
  mso-border-left-alt:solid windowtext .5pt !msorm;mso-border-alt:solid windowtext .5pt !msorm;
  padding:0in 5.4pt 0in 5.4pt !msorm;height:.3in'>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'>Is loan
  subject to New York Subprime Test requirements?<b style='mso-bidi-font-weight:
  normal'> </b></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'><input type="checkbox" disabled="disabled" />  </span>Yes<span style='mso-spacerun:yes'>  
  </span></span><!--[if supportFields]><span style='font-family:"Arial","sans-serif"'><span
  style='mso-element:field-begin'></span><span
  style='mso-spacerun:yes'> </span>FORMCHECKBOX <span style='mso-element:field-separator'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><!--[if gte mso 9]><xml>
   <w:data>FFFFFFFF650000001400070043006800650063006B0033003600000000000000000000000000000000000000000000000000</w:data>
  </xml><![endif]--></span><!--[if supportFields]><span style='font-family:
  "Arial","sans-serif"'><span style='mso-element:field-end'></span></span><![endif]--><span
  style='font-family:"Arial","sans-serif"'><span
  style='mso-spacerun:yes'> <input type="checkbox" disabled="disabled" checked="checked"/> </span>No<b style='mso-bidi-font-weight:normal'><o:p></o:p></b></span></p>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></b></p>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'>If you
  answered yes above, complete the below-referenced New York Sub-Prime test
  requirement fields:<o:p></o:p></span></p>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></b></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=346 colspan=2 valign=top style='width:259.6pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;background:#D9D9D9;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1210'><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>ALL Loans<o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Date of
    earliest GFE issued to borrower (MM/DD/YY)<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><span
    style='mso-spacerun:yes'>  </span></span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2;mso-yfti-lastrow:yes'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>PMMS Rate<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><span
    style='mso-spacerun:yes'> </span></span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
    %</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></b></p>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'>In addition, please indicate the
  following:<o:p></o:p></span></b></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=346 colspan=2 valign=top style='width:259.6pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;background:#D9D9D9;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1208'><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>ARMs</span></b><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>ARM Index
    at time of last rate set date<span style='mso-spacerun:yes'>  </span>­­­­­<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
    %</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Margin<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
    %</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:3;mso-yfti-lastrow:yes'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Note Rate<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
    </span><span style='font-family:"Arial","sans-serif"'>%<o:p></o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=346 colspan=2 valign=top style='width:259.6pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;background:#D9D9D9;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1209'><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>FIXED<o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Date of
    last commitment was issued to borrower (if Refi and no commitment issued,
    date of last lock with borrower) (MM/DD/YY)<o:p></o:p></span></p>
    <p class=TableText><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></b></p>
    <p class=TableText><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>NOTE</span></b><span
    style='font-family:"Arial","sans-serif"'>: Documentation must be in loan
    file to support this date<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1201'><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1201'><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1201'><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText align=center style='text-align:center;mso-prop-change:
    "Kelly Camp" 20131031T1201'><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><b
    style='mso-bidi-font-weight:normal'><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2;mso-yfti-lastrow:yes'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Date of
    TIL closest to the above (prior to not after) (MM/DD/YY)<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>/</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span> FORMTEXT <span style='mso-element:
    field-separator'></span></span><![endif]--><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><b
    style='mso-bidi-font-weight:normal'><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></b></p>
    </td>
   </tr>
  </table>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></b></p>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif"'>FHA Only </span></b><span
  style='font-family:"Arial","sans-serif"'>(where temporary/emergency
  regulation may have been applicable if standard test failed depending on
  closed date)<o:p></o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Adjusted
    APR (without life of loan MIP, calculated at 78%)<o:p></o:p></span></p>
    </td>
    <td width=173 valign=top style='width:129.8pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000014000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>
    %</span><b style='mso-bidi-font-weight:normal'><span style='font-family:
    "Arial","sans-serif"'><o:p></o:p></span></b></p>
    </td>
   </tr>
  </table>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>

   <tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=5 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
    <input type="button" value="Save Item 11, 12 and 13" id="btnSaveHighCostContinued" />
  </td>

 </tr>



 <tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:25.6pt;mso-prop-change:
  "JPMorgan Chase &amp; Co\." 20131008T1354' style='mso-yfti-irow:6 !msorm;
  mso-yfti-lastrow:yes !msorm;height:25.6pt !msorm'>
  <td width=734 colspan=2 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;border:solid windowtext 1.0pt !msorm;border-top:
  none !msorm;mso-border-top-alt:solid windowtext .5pt !msorm;mso-border-alt:
  solid windowtext .5pt !msorm;padding:0in 5.4pt 0in 5.4pt !msorm;height:25.6pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:10.0pt;mso-no-proof:yes'>Notes</span></b><span
  style='font-size:10.0pt;mso-no-proof:yes'>: <o:p></o:p></span></p>
  <ul style='margin-top:0in' type=disc>
   <li class=MsoNormal style='mso-list:l3 level1 lfo6'><span style='font-size:
       10.0pt;mso-no-proof:yes'>The dates and rate information required above are
       not based on when the file was registered, delivered or locked with
       Chase.<span style='mso-spacerun:yes'>  </span>This information is based
       on the same dates you (Correspondent) have used for the various
       regulatory requirements.<o:p></o:p></span></li>
  </ul>
  <p class=TableText style='margin-left:.5in;text-indent:-.25in;mso-list:l3 level1 lfo6'><![if !supportLists]><span
  style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
  Symbol'><span style='mso-list:Ignore'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:10.0pt'>The information
  contained in this form is not to be construed as legal or compliance advice,
  and is not meant to be used as a summary of the laws.<span
  style='mso-spacerun:yes'>  </span>If you have any questions related to this
  or any other law, you are strongly encouraged to contact your Legal and
  Compliance Counsel for further guidance.</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal><span style='font-size:10.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>

<span style='font-size:10.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
"Times New Roman";mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA'><br clear=all style='mso-special-character:line-break;
page-break-before:always'>
</span>

<p class=MsoNormal><span style='font-size:10.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>

<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:10.75pt'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
  height:10.75pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Part A:
  Regulatory Data Continued (as applicable)</span></b><b style='mso-bidi-font-weight:
  normal'><span style='font-size:10.0pt;font-family:"Arial","sans-serif";
  color:white'><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:17.3pt'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-family:"Arial","sans-serif";color:white'>Excluded Bonafide
  Discount Points - </span></b><span style='font-family:"Arial","sans-serif";
  color:white'>If<span style='mso-spacerun:yes'>  </span>you indicated on Page
  2<span style='mso-spacerun:yes'>  </span>that bonafide discount points were excluded,
  the following additional information must be provided (<i style='mso-bidi-font-style:
  normal'>only complete this portion if you excluded bonafide discount points
  from any of the Points and Fee tests (as applicable)</i></span><b
  style='mso-bidi-font-weight:normal'><span style='font-size:11.0pt;font-family:
  "Arial","sans-serif";color:white'><o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:.3in'>
  <td width=373 colspan=2 style='width:279.65pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>15.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Undiscounted
  interest rate (with all price/LLPA adjusters applicable to the borrower) or
  PAR interest rate with “0” points or (the higher of the two rates if there
  are two rates that are equidistant from “0” points)<o:p></o:p></span></p>
  <p class=MsoNormal style='mso-outline-level:1'><span style='font-size:10.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  </td>
  <td width=362 colspan=2 style='width:271.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText align=center style='text-align:center'><span
  style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
   mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
   .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=156 valign=top style='width:117.1pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Percent<o:p></o:p></span></p>
    </td>
    <td width=167 valign=top style='width:125.35pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><!--[if supportFields]><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="Item15Percent" />
    
    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF00000000140000000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'>%</span><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>
    <td width=156 valign=top style='width:117.1pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'>Amount<o:p></o:p></span></p>
    </td>
    <td width=167 valign=top style='width:125.35pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><span style='font-size:10.0pt;mso-bidi-font-size:11.0pt;
    font-family:"Arial","sans-serif"'>$ </span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="Item15Amount" />

    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF00000000140000000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=TableText align=center style='text-align:center'><span
  style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:.3in'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><b style='mso-bidi-font-weight:normal'><i
  style='mso-bidi-font-style:normal'><span style='font-size:10.0pt;font-family:
  "Arial","sans-serif"'>Note</span></i></b><i style='mso-bidi-font-style:normal'><span
  style='font-size:10.0pt;font-family:"Arial","sans-serif"'>: Closed loan file
  must contain supporting documentation for the undiscounted rate (e.g. correspondent’s
  rate sheet showing the undiscounted rate and lock period indicated for said
  rate).<span style='mso-spacerun:yes'>  </span>If you do not have rate sheets,
  please provide documentation that contains the above information (the same as
  you would provide to an auditor to validate how the bona fide points were
  derived)</span></i><span style='font-size:10.0pt;mso-bidi-font-size:11.0pt;
  font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;height:.3in'>
  <td width=373 colspan=2 style='width:279.65pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal style='margin-left:.25in;text-indent:-.25in;mso-outline-level:
  1;mso-list:l4 level1 lfo7'><![if !supportLists]><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Arial'><span
  style='mso-list:Ignore'>16.<span style='font:7.0pt "Times New Roman"'>&nbsp; </span></span></span><![endif]><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>If any discount
  points were determined to be bonafide and were excluded from the points and
  fee test, provide the Percent and Dollar Amount<span
  style='mso-spacerun:yes'>  </span>that was excluded for each test applicable<o:p></o:p></span></p>
  <p class=MsoNormal style='mso-outline-level:1'><span style='font-size:11.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoNormal style='margin-left:.25in;mso-outline-level:1'><b
  style='mso-bidi-font-weight:normal'><span style='font-size:10.0pt;font-family:
  "Arial","sans-serif"'>Reminder</span></b><span style='font-size:10.0pt;
  font-family:"Arial","sans-serif"'>: While there is no longer a standalone
  Agency 5% test for applications in 2014 (HOEPA standards apply), the Agencies
  do require Investment properties (any loans which are “exempt” from QM
  P&amp;F) to be run under HOEPA as well.<o:p></o:p></span></p>
  </td>
  <td width=362 colspan=2 style='width:271.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'>Indicate
  the percent and dollar amount of bonafide discount points excluded from each
  applicable test.<o:p></o:p></span></p>
  <p class=TableText style='margin-left:1.0in'><span style='font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
   style='margin-left:.05in;border-collapse:collapse;border:none;mso-border-alt:
   solid windowtext .5pt;mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;
   mso-border-insideh:.5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
    <td width=114 valign=top style='width:85.5pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>Test<o:p></o:p></span></b></p>
    </td>
    <td width=102 valign=top style='width:76.5pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>Percent Excluded<o:p></o:p></span></b></p>
    </td>
    <td width=126 valign=top style='width:94.25pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText><b style='mso-bidi-font-weight:normal'><span
    style='font-family:"Arial","sans-serif"'>Dollar Amount Excluded<o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=114 valign=top style='width:85.5pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><span style='font-family:"Arial","sans-serif"'>HOEPA/QM<o:p></o:p></span></p>
    </td>
    <td width=102 valign=top style='width:76.5pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="txtHOEPAQMPcnt" />
    
    
    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000000000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'>%</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
    <td width=126 valign=top style='width:94.25pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'>$</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span>
    FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="txtHOEPAQMAmount" />
    
    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000000000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2;mso-yfti-lastrow:yes'>
    <td width=114 valign=top style='width:85.5pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><span style='font-family:"Arial","sans-serif"'>State<o:p></o:p></span></p>
    </td>
    <td width=102 valign=top style='width:76.5pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span><span
    style='mso-spacerun:yes'> </span>FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="txtStatePcnt" />
    
    
    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000000000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'>%</span><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
    <td width=126 valign=top style='width:94.25pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=TableText style='line-height:200%'><span style='font-size:10.0pt;
    mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'>$</span><!--[if supportFields]><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-element:field-begin'></span>
    FORMTEXT <span style='mso-element:field-separator'></span></span><![endif]--><span
    style='font-size:10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;
    font-family:"Arial","sans-serif"'><span style='mso-no-proof:yes'>
    
    <input type="text" style="width:3em" id="txtStateAmount" />
    
    </span><!--[if gte mso 9]><xml>
     <w:data>FFFFFFFF0000000000000500540065007800740033000000000000000000000000000000000000000000000000000000</w:data>
    </xml><![endif]--></span><!--[if supportFields]><span style='font-size:
    10.0pt;mso-bidi-font-size:11.0pt;line-height:200%;font-family:"Arial","sans-serif"'><span
    style='mso-element:field-end'></span></span><![endif]--><span
    style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=TableText><span style='font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>

   <tr style='mso-yfti-irow:13;height:45.4pt'>
  <td width=445 colspan=5 style='width:333.9pt;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:45.4pt'>
    <input type="button" value="Save Item 15 and 16" id="btnExcludedBonafide" />
  </td>

 </tr>


 <tr style='mso-yfti-irow:5;height:.3in'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.3in'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:10.0pt;mso-no-proof:yes'>Notes</span></b><span
  style='font-size:10.0pt;mso-no-proof:yes'>: <o:p></o:p></span></p>
  <ul style='margin-top:0in' type=disc>
   <li class=MsoNormal style='mso-list:l3 level1 lfo6'><span style='font-size:
       10.0pt;mso-no-proof:yes'>The dates and rate information required above are
       not based on when the file was registered, delivered or locked with
       Chase.<span style='mso-spacerun:yes'>  </span>This information is based
       on the same dates you (Correspondent) have used for the various
       regulatory requirements.<o:p></o:p></span></li>
  </ul>
  <p class=MsoNormal><span style='font-size:10.0pt;mso-no-proof:yes'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoNormal style='margin-left:.5in;text-indent:-.25in;mso-list:l3 level1 lfo6'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:10.0pt;mso-no-proof:
  yes'>The information contained in this form is not to be construed as legal
  or compliance advice, and is not meant to be used as a summary of the
  laws.<span style='mso-spacerun:yes'>  </span>If you have any questions
  related to this or any other law, you are strongly encouraged to contact your
  Legal and Compliance Counsel for further guidance.</span><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  </td>
 </tr>

 </table>
 <br />
 <br />
 <br />
 <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>

 <tr style='mso-yfti-irow:6;height:10.75pt'>
  <td width=637 colspan=3 style='width:477.9pt;border:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#006EC7;padding:0in 5.4pt 0in 5.4pt;height:10.75pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Part B:
  Special Features – Please check the appropriate box for loan special features.
  If loan does not contain one of the below-referenced special features, leave
  this section blank.<o:p></o:p></span></b></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:solid windowtext 1.0pt;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
  height:10.75pt'>
  <p class=MsoNormal align=center style='text-align:center'><b
  style='mso-bidi-font-weight:normal'><span style='font-size:10.0pt;font-family:
  "Arial","sans-serif";color:white'>For Internal Use Only<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:7;height:17.3pt'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.3pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Underwriting
  Special Features<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:8;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>

  <input type="checkbox" disabled="disabled" />

  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Auction
  House <o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>T770<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:9;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>

<input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Construction
  Conversion mortgages, as defined by Freddie Mac bulletin 11/20/06<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X893<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:10;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Deed
  Restriction – Survive Foreclosure<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X889<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:11;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Deed
  Restriction – Terminate with Foreclosure<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X894<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:12;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" id="SFFannieMae" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Fannie
  Mae DU Refi PlusTM<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X908<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:13;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" id="SFFreddieMac" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Freddie
  Mac LP Open Access <o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X908<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:14;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Fannie
  Mae Property Inspection Waiver (PIW)<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>N/A<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:15;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Housing
  Assistance Program (must be approved by Chase)<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>N/A<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:16;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Newly
  built home, as defined by Freddie Mac bulletin 11/20/06<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X892<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:17;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Renovation/Rehab
  of Existing Property, as defined by Freddie Mac bulletin 11/20/06<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X900<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:18;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Texas
  Cash-out Refinance<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>N/A<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:19;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Unexpired
  Redemption Rights<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>T845<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:20;height:17.5pt'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:17.5pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>MyCommunityMortgage<sup>R
  </sup>(MCM) Special Features<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:21;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Affordable
  Housing Second<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X090<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:22;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Cash
  on Hand (used for closing)<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X098<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:23;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Section
  8 Housing<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X873<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:24;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Interested
  Party Contributions<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X886<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:25;height:17.05pt'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:17.05pt'>
    <input type="checkbox" disabled="disabled" />
  </td>
  <td width=600 colspan=2 style='width:6.25in;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:17.05pt'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Flexibilities
  for community employees (MCM Community Solutions)<o:p></o:p></span></p>
  </td>
  <td width=97 style='width:72.9pt;border-top:none;border-left:none;border-bottom:
  solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;mso-border-top-alt:
  solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
  solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:17.05pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif"'>X895<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:26;height:16.6pt'>
  <td width=734 colspan=4 style='width:7.65in;border:solid windowtext 1.0pt;
  border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  background:#66BC29;padding:0in 5.4pt 0in 5.4pt;height:16.6pt'>
  <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
  style='font-size:11.0pt;font-family:"Arial","sans-serif";color:white'>Mortgage
  Insurance Options<o:p></o:p></span></b></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:27;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" id="MIMonthlyPremium" />
  </td>
  <td width=697 colspan=3 style='width:522.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Mortgage
  Insurance – Monthly Premium<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:28;mso-yfti-lastrow:yes;height:.2in'>
  <td width=37 style='width:27.9pt;border:solid windowtext 1.0pt;border-top:
  none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:solid windowtext .5pt;
  padding:0in 5.4pt 0in 5.4pt;height:.2in'>
    <input type="checkbox" id="MISinglePremium" />
  </td>
  <td width=697 colspan=3 style='width:522.9pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
  mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt;height:.2in'>
  <p class=MsoNormal><span style='font-size:11.0pt;font-family:"Arial","sans-serif"'>Mortgage
  Insurance – Single Premium<o:p></o:p></span></p>
  </td>
 </tr>
 <![if !supportMisalignedColumns]>
 <tr height=0>
  <td width=37 style='border:none'></td>
  <td width=315 style='border:none'></td>
  <td width=258 style='border:none'></td>
  <td width=97 style='border:none'></td>
 </tr>
 <![endif]>
</table>

<p class=MsoNormal><span style='font-size:10.0pt;font-family:"Arial","sans-serif"'><o:p><input type="button" value="Save Special Feature" id="btnSaveSpecialFeature" /></o:p></span></p>

<span style='font-size:10.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:
"Times New Roman";mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA'><br clear=all style='mso-special-character:line-break;
page-break-before:always'>
</span>

<p class=MsoNormal><span style='font-size:10.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>

<!--  start -->

<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width=734
 style='border-collapse:collapse;mso-table-layout-alt:fixed;border:none;
 mso-border-alt:solid windowtext .5pt;mso-yfti-tbllook:1184;mso-padding-alt:
 0in 5.4pt 0in 5.4pt;mso-border-insideh:.5pt solid windowtext;mso-border-insidev:
 .5pt solid windowtext'>
 <thead>
  <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:16.15pt'>
   <td width=734 colspan=2 style='width:7.65in;border:solid windowtext 1.0pt;
   mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
   height:16.15pt'>
   <p class=MsoNormal><b style='mso-bidi-font-weight:normal'><span
   style='font-size:11.0pt;mso-bidi-font-size:10.0pt;font-family:"Arial","sans-serif";
   color:white'>Follow the delivery instructions provided below.<o:p></o:p></span></b></p>
   </td>
  </tr>
  <tr style='mso-yfti-irow:1;height:12.1pt'>
   <td width=127 style='width:95.4pt;border:solid windowtext 1.0pt;border-top:
   none;mso-border-top-alt:solid windowtext 1.0pt;background:#66BC29;
   padding:0in 5.4pt 0in 5.4pt;height:12.1pt'>
   <p class=MsoBlockText><b><span style='font-size:10.0pt;font-family:"Arial","sans-serif";
   color:white'>IF delivering via…<o:p></o:p></span></b></p>
   </td>
   <td width=607 style='width:455.4pt;border-top:none;border-left:none;
   border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
   mso-border-top-alt:solid windowtext 1.0pt;background:#66BC29;padding:0in 5.4pt 0in 5.4pt;
   height:12.1pt'>
   <p class=MsoBlockText><b><span style='font-size:10.0pt;font-family:"Arial","sans-serif";
   color:white'>THEN…<o:p></o:p></span></b></p>
   </td>
  </tr>
 </thead>
 <tr style='mso-yfti-irow:2;height:130.85pt'>
  <td width=127 valign=top style='width:95.4pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:130.85pt'>
  <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>ChaseLoanManager
  Imaged Document Delivery<b><o:p></o:p></b></span></p>
  </td>
  <td width=607 valign=top style='width:455.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:130.85pt'>
  <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Upload
  documents under the appropriate <b style='mso-bidi-font-weight:normal'>Package
  Type</b>:<o:p></o:p></span></p>
  <p class=MsoBlockText><span style='font-size:3.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width=564
   style='margin-left:16.85pt;border-collapse:collapse;mso-table-layout-alt:
   fixed;border:none;mso-border-alt:solid windowtext .5pt;mso-yfti-tbllook:
   1184;mso-padding-alt:0in 0in 0in 0in;mso-border-insideh:.5pt solid windowtext;
   mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.1pt'>
    <td width=354 style='width:265.5pt;border:solid windowtext 1.0pt;
    mso-border-alt:solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
    height:12.1pt'>
    <p class=MsoBlockText><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    color:white'>IF delivering…<o:p></o:p></span></b></p>
    </td>
    <td width=210 style='width:157.5pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
    height:12.1pt'>
    <p class=MsoBlockText><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    color:white'>THEN Select Package Type of…<o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=354 valign=top style='width:265.5pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    mso-bidi-font-weight:bold'>Credit and closed documents for funding review<o:p></o:p></span></p>
    </td>
    <td width=210 valign=top style='width:157.5pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p style='margin:0in;margin-bottom:.0001pt'><span style='font-size:9.0pt'>Full
    file<o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2'>
    <td width=354 valign=top style='width:265.5pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    mso-bidi-font-weight:bold'>Closed only documents for funding review
    (underwriting credit package previously submitted)<o:p></o:p></span></p>
    </td>
    <td width=210 valign=top style='width:157.5pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p style='margin:0in;margin-bottom:.0001pt'><span style='font-size:9.0pt'>Closed
    Pkge Only<o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:3;mso-yfti-lastrow:yes'>
    <td width=354 valign=top style='width:265.5pt;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    mso-bidi-font-weight:bold'>Appraisal data:<o:p></o:p></span></p>
    <p class=MsoBlockText style='margin-left:29.25pt;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
    style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif";mso-bidi-font-weight:bold'>AI Ready<sup>TM</sup> XML</span><span
    style='font-size:9.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
    <p class=MsoBlockText style='margin-left:29.25pt;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
    style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif"'>MISMO XML<o:p></o:p></span></p>
    <p class=MsoBlockText style='margin-left:29.25pt;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol;mso-bidi-font-weight:bold'><span
    style='mso-list:Ignore'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif"'>1st Generation PDF<span style='mso-bidi-font-weight:
    bold'><o:p></o:p></span></span></p>
    <p class=MsoBlockText style='text-autospace:ideograph-numeric ideograph-other'><span
    style='font-size:9.0pt;font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
    <p class=MsoBlockText><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Note:</span></b><span
    style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
    bold'> If Appraisal Score Report already obtained through FNC, click <b>Appraisal
    Pre-Delivery Order #</b> button on Search Results screen and enter <b>Appraisal
    Pre-Delivery Order #</b> in open field.<o:p></o:p></span></p>
    </td>
    <td width=210 valign=top style='width:157.5pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    mso-bidi-font-weight:bold'>Appraisal Data/1st Gen<o:p></o:p></span></p>
    </td>
   </tr>
  </table>
  <p style='margin:0in;margin-bottom:.0001pt'><span style='font-size:9.0pt;
  mso-bidi-font-weight:bold'><o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;height:22.4pt'>
  <td width=127 valign=top style='width:95.4pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:22.4pt'>
  <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>FTP
  or Connectivity Services Imaged Document Delivery<o:p></o:p></span></p>
  </td>
  <td width=607 style='width:455.4pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:22.4pt'>
  <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Create
  imaged file and upload according to <b style='mso-bidi-font-weight:normal'>Indexed</b>
  or <b style='mso-bidi-font-weight:normal'>Non-Indexed</b> delivery
  requirements<o:p></o:p></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4;mso-yfti-lastrow:yes;height:263.15pt'>
  <td width=127 valign=top style='width:95.4pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:263.15pt'>
  <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Mail
  or Overnight:<o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Hard
  Copy <b style='mso-bidi-font-weight:normal'>OR</b> <o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Original
  Note with required attachments (i.e. Wiring Instructions, Bailee Letter,
  Addendums)<o:p></o:p></span></p>
  </td>
  <td width=607 valign=top style='width:455.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:263.15pt'>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Acco-fasten
  all documents to the file folder</span><span style='font-size:9.0pt;
  font-family:"Arial","sans-serif";mso-fareast-font-family:Calibri'><o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Place
  a label on the file folder tab with borrower’s name, Chase Loan Number, and
  Commitment Number, if applicable<o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Address
  and ship loan package to the following address:<o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in'><span style='font-size:3.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in'><span style='font-size:3.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoBlockText align=center style='text-align:center'><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
  bold'>Chase Record Center<o:p></o:p></span></p>
  <p class=MsoBlockText align=center style='text-align:center'><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
  bold'>RE:<span style='mso-spacerun:yes'>  </span>Correspondent Files<o:p></o:p></span></p>
  <p class=MsoBlockText align=center style='text-align:center'><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
  bold'>Mail Code:<span style='mso-spacerun:yes'>  </span>LA4-4774<o:p></o:p></span></p>
  <p class=MsoBlockText align=center style='text-align:center'><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
  bold'>700 Kansas Lane<o:p></o:p></span></p>
  <p class=MsoBlockText align=center style='text-align:center'><span
  style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
  bold'>Monroe, LA 71203</span><span style='font-size:3.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in'><span style='font-size:3.0pt;
  font-family:"Arial","sans-serif"'><o:p>&nbsp;</o:p></span></p>
  <p class=MsoNormal><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
  mso-fareast-font-family:Calibri'>Notes:</span></b><span style='font-size:
  9.0pt;font-family:"Arial","sans-serif";mso-fareast-font-family:Calibri;
  mso-bidi-font-weight:bold'><o:p></o:p></span></p>
  <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;mso-list:
  l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
  style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
  mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
  style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </span></span></span><![endif]><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Also
  do the following based on document(s) being delivered:<o:p></o:p></span></p>
  <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width=564
   style='margin-left:16.85pt;border-collapse:collapse;mso-table-layout-alt:
   fixed;border:none;mso-border-alt:solid windowtext .5pt;mso-yfti-tbllook:
   1184;mso-padding-alt:0in 0in 0in 0in;mso-border-insideh:.5pt solid windowtext;
   mso-border-insidev:.5pt solid windowtext'>
   <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:12.1pt'>
    <td width=168 style='width:1.75in;border:solid windowtext 1.0pt;mso-border-alt:
    solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
    height:12.1pt'>
    <p class=MsoBlockText><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    color:white'>IF delivering…<o:p></o:p></span></b></p>
    </td>
    <td width=396 style='width:297.0pt;border:solid windowtext 1.0pt;
    border-left:none;mso-border-left-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;background:#006EC7;padding:0in 5.4pt 0in 5.4pt;
    height:12.1pt'>
    <p class=MsoBlockText><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    color:white'>THEN…<o:p></o:p></span></b></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:1'>
    <td width=168 valign=top style='width:1.75in;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif"'>Credit-only
    packages for Chase underwriting review</span><span style='font-size:9.0pt;
    font-family:"Arial","sans-serif";mso-fareast-font-family:Calibri;
    mso-bidi-font-weight:bold'><o:p></o:p></span></p>
    </td>
    <td width=396 valign=top style='width:297.0pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText style='text-autospace:ideograph-numeric ideograph-other'><span
    style='font-size:9.0pt;font-family:"Arial","sans-serif"'>do not include in the
    same package as files for <br>
    funding review<o:p></o:p></span></p>
    </td>
   </tr>
   <tr style='mso-yfti-irow:2;mso-yfti-lastrow:yes'>
    <td width=168 valign=top style='width:1.75in;border:solid windowtext 1.0pt;
    border-top:none;mso-border-top-alt:solid windowtext .5pt;mso-border-alt:
    solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
    mso-fareast-font-family:Calibri;mso-bidi-font-weight:bold'>Original Note</span><span
    style='font-size:9.0pt;font-family:"Arial","sans-serif";mso-bidi-font-weight:
    bold'><o:p></o:p></span></p>
    </td>
    <td width=396 valign=top style='width:297.0pt;border-top:none;border-left:
    none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
    mso-border-top-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;
    mso-border-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>
    <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
    style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif"'>place Original Note on top of documents or flag it
    within file, if shipped with funding documents<o:p></o:p></span></p>
    <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
    style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif"'>include Chase Loan Number or Commitment Number with
    Original Note, if being shipped separately from funding documents, so it
    can be matched to funding documents<o:p></o:p></span></p>
    <p class=MsoBlockText style='margin-left:.25in;text-indent:-.25in;
    mso-list:l6 level1 lfo4;text-autospace:ideograph-numeric ideograph-other'><![if !supportLists]><span
    style='font-size:9.0pt;font-family:Symbol;mso-fareast-font-family:Symbol;
    mso-bidi-font-family:Symbol'><span style='mso-list:Ignore'>·<span
    style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </span></span></span><![endif]><span style='font-size:9.0pt;font-family:
    "Arial","sans-serif"'>instruct your Warehouse Bank to identify Original
    Note with Chase Loan Number or Commitment Number, if using a Warehouse
    Line, <br>
    so it can be matched to funding documents<o:p></o:p></span></p>
    </td>
   </tr>
  </table>
  <p class=MsoNormal><b><span style='font-size:9.0pt;font-family:"Arial","sans-serif";
  mso-fareast-font-family:Calibri'><o:p></o:p></span></b></p>
  </td>
 </tr>
</table>

<p class=MsoNormal><span style='font-size:6.0pt;font-family:"Arial","sans-serif";
mso-bidi-font-weight:bold'><o:p>&nbsp;</o:p></span></p>

</div>

<%--</body>--%>

</asp:Content>
