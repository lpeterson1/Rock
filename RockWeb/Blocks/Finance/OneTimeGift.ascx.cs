﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Rock.Attribute;
using Rock.Model;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.Finance
{
    /// <summary>
    /// 
    /// </summary>    
    [CustomCheckboxListField( "Default Funds to display", "Which funds should be displayed by default?",
        "SELECT [Name] AS [Text], [Id] AS [Value] FROM [Fund] WHERE [IsActive] = 1 ORDER BY [Order]", true, "", "Filter", 1 )]
    [BooleanField( "Stack layout vertically", "Should giving UI be stacked vertically or horizontally?", false, "UI Options", 2 )]
    [BooleanField( "Show Campus selection", "Should giving be associated with a specific campus?", false, "UI Options", 3 )]    
    public partial class OneTimeGift : RockBlock
    {
        #region Fields

        protected bool _UseStackedLayout = false;
        protected bool _ShowCampusSelect = false;
        protected bool _ShowSaveDetails = false;
        protected string spanClass = "";

        protected FundService _fundService = new FundService();
        
        protected List<FinancialTransactionFund> _fundList = new List<FinancialTransactionFund>();
        protected FinancialTransactionService _transactionService = new FinancialTransactionService();
        protected FinancialTransaction _transaction = new FinancialTransaction();
        protected Dictionary<string, decimal> _giftList = new Dictionary<string, decimal>();
        
        #endregion

        #region Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            _UseStackedLayout = Convert.ToBoolean( GetAttributeValue( "Stacklayoutvertically" ) );
            _ShowCampusSelect = Convert.ToBoolean( GetAttributeValue( "ShowCampusselection" ) );
                        
            if ( CurrentPerson != null )
            {
                _ShowSaveDetails = true;
            }

            if ( _ShowCampusSelect )
            {
                BindCampuses();
            }

            if ( !IsPostBack )
            {
                BindFunds();
                BindCreditOptions();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            nbMessage.Visible = false;

            base.OnLoad( e );
        }

        #endregion

        #region Edit Events

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnNext_Click( object sender, EventArgs e )
        {
            PersonService personService = new PersonService();
            var personGroup = personService.GetByEmail( Request.Form["txtEmail"] );
            Person person;

            if ( personGroup.Count() > 0 )
            {
                person = personGroup.Where( p => p.FirstName == Request.Form["txtFirstName"]
                    && p.LastName == Request.Form["txtLastName"] ).Distinct().FirstOrDefault();
                //other duplicate person handling here?  see NewAccount.ascx DisplayDuplicates()
            }
            else
            {
                person = new Person();
                personService.Add( person, CurrentPersonId );
            }

            person.Email = Request.Form["txtEmail"];
            person.GivenName = Request.Form["txtFirstName"];
            person.LastName = Request.Form["txtFirstName"];

            personService.Save( person, CurrentPersonId );

            using ( new Rock.Data.UnitOfWorkScope() )
            {
                //_giftList.Clear();
                var lookupID = _fundService.Queryable().Where( f => f.IsActive )
                    .Distinct().OrderBy( f => f.Order ).ToDictionary( f => f.PublicName, f => f.Id );
                
                _transactionService = new FinancialTransactionService();
                _fundList = new List<FinancialTransactionFund>();                              
                
                var transaction = _transactionService.Get( _transaction.Id  );
                if ( transaction == null )
                {
                    transaction = new FinancialTransaction();                    
                    _transactionService.Add( transaction, CurrentPersonId );
                }

                foreach ( RepeaterItem item in rptFundList.Items )
                {
                    //_giftList.Add( ( (HtmlInputControl)item.FindControl( "btnFundName" ) ).Value
                    //, Convert.ToDecimal( ( (HtmlInputControl)item.FindControl( "inputFundAmount" ) ).Value ) );
                    FinancialTransactionFund fund = new FinancialTransactionFund();
                    fund.FundId = lookupID[( (HtmlInputControl)item.FindControl( "btnFundName" ) ).Value];
                    fund.Amount = Convert.ToDecimal( ( (HtmlInputControl)item.FindControl( "inputFundAmount" ) ).Value );
                    fund.TransactionId = transaction.Id;
                    _fundList.Add( fund );
                }
                
                transaction.EntityId = person.Id;
                transaction.EntityTypeId = new Rock.Model.Person().TypeId;

                transaction.Amount = _fundList.Sum( fA => (decimal)fA.Amount );
            }
            
            cfrmName.Text = person.FullName;
            cfrmTotal.Text = _fundList.Sum( g => g.Amount ).ToString();
            var paymentMethod = listCardTypes.Attributes["class"].Split(' ');

            if ( paymentMethod.Count() > 1 )
            {
                // using credit card 
                lblPaymentType.Text = paymentMethod.ElementAtOrDefault( 2 );

            }
            else
            {
                // using ACH
                lblPaymentType.Text = radioAccountType.SelectedValue;
            }


            string lastFour = Request.Form["numCreditCard"];
            if ( lastFour != null ) 
            {
                lblPaymentLastFour.Text = Request.Form["numCreditCard"].Substring( 16, 4 );
            }

            rptGiftConfirmation.DataSource = _fundList.ToDictionary( f => f.Amount, f => f.Fund );

            //rptGiftConfirmation.DataSource = _transactions;
            //rptGiftConfirmation.DataBind();            
            
            pnlDetails.Visible = false;
            pnlConfirm.Visible = true;
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnBack_Click( object sender, EventArgs e )
        {            
            pnlConfirm.Visible = false;
            pnlDetails.Visible = true;            
        }
        
        /// <summary>
        /// Handles the Click event of the btnAddFund control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddFund_SelectionChanged( object sender, EventArgs e )
        {
            _giftList.Clear();
            
            foreach (RepeaterItem item in rptFundList.Items)
            {
                _giftList.Add( ( (HtmlInputControl)item.FindControl( "btnFundName" ) ).Value
                    , Convert.ToDecimal( ( (HtmlInputControl)item.FindControl( "inputFundAmount" ) ).Value ));
            }

            // initialize new contribution
            _giftList.Add( btnAddFund.SelectedValue, 0M );

            if ( btnAddFund.Items.Count > 1 )
            {
                btnAddFund.Items.Remove(btnAddFund.SelectedValue);
                btnAddFund.Title = "Add Another Gift";
            }
            else
            {
                divAddFund.Visible = false;
            }

            rptFundList.DataSource = _giftList;
            rptFundList.DataBind();
        }

        protected void btnGive_Click( object sender, EventArgs e )
        {
            FinancialTransaction gift = _transactionService.Get( _transaction.Id );

            _transactionService.Save( gift, CurrentPersonId );
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Binds the campuses.
        /// </summary>
        protected void BindCampuses()
        {
            ddlCampusList.Items.Clear();
            CampusService campusService = new CampusService();
            var items = campusService.Queryable().OrderBy( a => a.Name ).Select( a => a.Name ).Distinct().ToList();

            foreach ( string item in items )
            {
                ddlCampusList.Items.Add( item + " Campus");                
            }

            ddlCampusList.Title = "Select Your Campus";
        }

        /// <summary>
        /// Binds the funds.
        /// </summary>
        protected void BindFunds( )
        {
            var queryable = _fundService.Queryable().Where( f => f.IsActive )
                .Distinct().OrderBy( f => f.Order );

            List<int> defaultFunds = GetAttributeValue( "DefaultFundstodisplay" ).Any()
                ? GetAttributeValue( "DefaultFundstodisplay" ).Split( ',' ).ToList().Select( s => int.Parse( s ) ).ToList()
                : new List<int>( ( queryable.Select( f => f.Id ).ToList().FirstOrDefault() ) );
            
            if ( ( queryable.Count() - defaultFunds.Count ) > 0 )
            {
                btnAddFund.DataSource = queryable.Where( f => !defaultFunds.Contains( f.Id ) )
                   .Select( f => f.PublicName ).ToList();
                btnAddFund.DataBind();
                btnAddFund.Title = "Add Another Gift";
                divAddFund.Visible = true;             
            }
            else
            {
                divAddFund.Visible = false;
            }

            rptFundList.DataSource = queryable.Where( f => defaultFunds.Contains( f.Id ) )
                .ToDictionary( f => f.PublicName, f => Convert.ToDecimal( !f.IsActive ) );
            rptFundList.DataBind();            
        }

        /// <summary>
        /// Binds the credit options.
        /// </summary>
        protected void BindCreditOptions()
        {
            btnYearExpiration.Items.Clear();
            btnYearExpiration.Items.Add( DateTime.Now.Year.ToString() );

            for (int i = 1; i <= 12; i++)
            {
                btnMonthExpiration.Items.Add( CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName( i ) );
                btnYearExpiration.Items.Add(DateTime.Now.AddYears(i).Year.ToString());
            }

            btnMonthExpiration.DataBind();
            btnYearExpiration.DataBind();
        }

        #endregion
    }
}