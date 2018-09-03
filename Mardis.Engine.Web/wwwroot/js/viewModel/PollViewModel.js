//
function PollViewModel() {
    var self = this;

    self.IdSection = ko.observable();
    self.Title = ko.observable();
    self.ItemsQuestion = ko.observableArray();
    self.Weight = ko.observable();

}

//
function QuestionViewModel() {
    var self = this;

    self.IdQuestion = ko.observable();
    self.Title = ko.observable();
    self.Weight = ko.observable();
    self.IdTypePoll = ko.observable();
    self.ItemsAnswer = ko.observableArray();
    self.HasPhotos = ko.observable();
    self.CountPhotos = ko.observable();

}

//
function AnswerViewModel() {
    var self = this;

    self.IdAnswer = ko.observable();
    self.Title = ko.observable();
    self.Weight = ko.observable();
}