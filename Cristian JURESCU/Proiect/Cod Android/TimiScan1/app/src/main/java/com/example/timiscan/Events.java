package com.example.timiscan;

public class Events {

    private String name;
    private String data;
    private String type;
    private String description;
    private String location;

    public Events(String name, String data, String type, String description, String location) {
        this.name = name;
        this.data = data;
        this.type = type;
        this.description = description;
        this.location = location;
    }

    public Events() {}


    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }
}

