package com.example.timiscan;

public class Objective {

    private String description;
    private String history;
    private String program;


    public Objective(String description, String history, String program) {
        this.description = description;
        this.history = history;
        this.program = program;
    }

    public Objective() {
    }

    public String getDescription() {
        return description;
    }

    public String getHistory() {
        return history;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setHistory(String history) {
        this.history = history;
    }

    public String getProgram() {
        return program;
    }

    public void setProgram(String program) {
        this.program = program;
    }
}
