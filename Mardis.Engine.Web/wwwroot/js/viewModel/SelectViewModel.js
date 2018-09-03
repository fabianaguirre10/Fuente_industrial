function SelectViewModel(id,name) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
}