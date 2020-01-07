'use strict';

function TrainingModel(id, topic, description, scheduledOn, trainerId, maxAttendees, currentAttendees, location, durationHours) {
  this.id = id;
  this.topic = topic;
  this.description = description;
  this.scheduledOn = scheduledOn;
  this.trainerId = trainerId;
  this.maxAttendees = maxAttendees;
  this.currentAttendees = currentAttendees;
  this.location = location;
  this.durationHours = durationHours;
}

module.exports = TrainingModel;
