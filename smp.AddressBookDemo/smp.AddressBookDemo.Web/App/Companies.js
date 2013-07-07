app.Companies = {};

(function(self) {

    var companiesModel = function() {
        var modelSelf = this;

        modelSelf.FirstLetters = ko.observableArray([
                {letter:'A'},
                {letter:'B'},
                {letter:'C'},
                {letter:'D'},
                {letter:'E'},
                {letter:'F'},
                {letter:'G'},
                {letter:'H'},
                {letter:'I'},
                {letter:'J'},
                {letter:'K'},
                {letter:'L'},
                {letter:'M'},
                {letter:'N'},
                {letter:'O'},
                {letter:'P'},
                {letter:'Q'},
                {letter:'R'},
                {letter:'S'},
                {letter:'T'},
                {letter:'U'},
                {letter:'V'},
                {letter:'W'},
                {letter:'X'},
                {letter:'Y'},
                {letter:'Z'}
            ]);
        modelSelf.Companies = ko.observableArray();
        modelSelf.FirstLetter = ko.observable("A");

        modelSelf.Count = ko.computed(function() {
            return modelSelf.Companies().length;
        });

        modelSelf.SelectLetter = function(firstLetter) {
            modelSelf.FirstLetter(firstLetter.letter);
            modelSelf.LoadData();
        };

        modelSelf.AddNewCompany = function() {
            $.ajax({
                url: '/companies/addnew',
                type: 'GET',
                dataType: 'json',
                success: function(result) {
                    window.location = '/companies/Detail/' + result;
                },
                error: function () {
                    app.ShowError("Error", "Sorry an error creating a new company");
                }
            });
        };

        modelSelf.SelectCompany = function(company) {
            window.location = '/companies/Detail/' + company.Id();
        };
            
        modelSelf.LoadData = function() {
            $.ajax({
                url: '/companies/namestarting/' + modelSelf.FirstLetter(),
                type: 'GET',
                dataType: 'json',
                success: function(result) {
                    modelSelf.Companies().length = 0;
                    ko.mapping.fromJS(result, {}, modelSelf);
                },
                error: function () {
                    app.ShowError("Error", "Sorry an error occured loading the data");
                }
            });
        };

        modelSelf.LoadData();
    };
    
    self.init = function() {
        ko.applyBindings(new companiesModel(), $('#companiesContainer')[0]);
    };

})(app.Companies);