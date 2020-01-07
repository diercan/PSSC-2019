package com.example.timiscan;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.FirebaseFirestore;

import java.util.HashMap;
import java.util.Map;

public class CreateEvent extends AppCompatActivity {
    private static final String TAG = "DocSnippets";
    private FirebaseFirestore db;
    Button addButton;
    EditText name;
    EditText data;
    EditText location;
    EditText description;
    EditText categoty;
    String Name;
    String Data;
    String Location;
    String Description;
    String Category;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_event);

        addButton = findViewById(R.id.addbutton);
        name = findViewById(R.id.editText);
        data = findViewById(R.id.editText2);
        location = findViewById(R.id.editText3);
        description = findViewById(R.id.editText4);
        categoty = findViewById(R.id.editText5);

        db = FirebaseFirestore.getInstance();
    }

    public void AddEvent(View view)
    {
        Name = name.getText().toString();
        Data = data.getText().toString();
        Location = location.getText().toString();
        Description = description.getText().toString();
        Category = categoty.getText().toString();

        Map<String, Object> data = new HashMap<>();
        data.put("name", Name);
        data.put("data", Data);
        data.put("description", Description);
        data.put("location", Location);
        data.put("type", Category);

        db.collection("events")
                .add(data)
                .addOnSuccessListener(new OnSuccessListener<DocumentReference>() {
                    @Override
                    public void onSuccess(DocumentReference documentReference) {
                        Log.d(TAG, "DocumentSnapshot written with ID: " + documentReference.getId());
                        Toast.makeText(CreateEvent.this, "Event is added.", Toast.LENGTH_LONG).show();
                    }
                })
                .addOnFailureListener(new OnFailureListener() {
                    @Override
                    public void onFailure(@NonNull Exception e) {
                        Log.w(TAG, "Error adding document", e);
                    }
                });
    }
}
