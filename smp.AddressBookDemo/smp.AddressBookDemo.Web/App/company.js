app.company = {};

(function(self) {

    var companyModel = function(data) {
        var modelSelf = this;

        modelSelf.EditingCompany = ko.observable(false);
        modelSelf.Contacts = ko.observableArray();
        modelSelf.Name = ko.observable();
        
        ko.mapping.fromJS(data, { }, modelSelf);

        modelSelf.DisplayCompany = ko.computed(function() {
            return !modelSelf.EditingCompany();
        });

        modelSelf.EditCompany = function() {
            modelSelf.EditingCompany(true);
        };

        modelSelf.CancelEditCompany = function() {
            modelSelf.EditingCompany(false);
        };
        
        modelSelf.SearchPostCode = function() {
            $.ajax({
                url: 'http://www.tabcat.co.uk/postcode_lookup_json_v2.php',
                dataType: 'jsonp',
                data: 'postcode=' + modelSelf.PostCode() + '&number=',
                jsonp: 'jsonp_callback',
                success: function (result) {
                    if (data.length == 0)
                        return;
                    modelSelf.AddressLine1(result[0].address);
                    modelSelf.Town(result[0].town);
                    modelSelf.County(result[0].county);
                },
                error: function(){
                    app.ShowError("Error", "Sorry an error occured when trying to search for the address.");
                }
            });
        };

        modelSelf.SaveCompany = function() {
            $.ajax({
                url: '/companies/save',
                dataType: 'json',
                type: 'POST',
                data: ko.mapping.toJS(modelSelf),
                success: function (result) {
                    if(result) {
                        $('#SavedDetails').fadeIn(250);
                        $('#detailsTitle').html(modelSelf.Name() + ' <i class="icon-chevron-down hide"></i>');
                        setTimeout(function() {
                            $('#SavedDetails').fadeOut(250);
                        }, 3000);
                    } else {
                        app.ShowError("Error", "Sorry, unable to save changes at this time.");
                    }
                },
                error: function(){
                    app.ShowError("Error", "Sorry an error occured when trying to search for the address.");
                }
            });
        };
    };
    
    self.init = function (data) {
        ko.applyBindings(new companyModel (data), $('#companyContainer')[0]);

        app.RegisterCollapsibleLegend();
    };

})(app.company);