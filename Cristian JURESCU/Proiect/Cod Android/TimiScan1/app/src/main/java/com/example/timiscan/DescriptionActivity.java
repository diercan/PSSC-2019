package com.example.timiscan;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.timiscan.fragments.EventFragment;
import com.example.timiscan.fragments.ScanFragment;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

import java.util.Objects;

public class DescriptionActivity extends AppCompatActivity {

    private static final String TAG = "DocSnippets";
    public static final String EventsForObjective = "EVENTS_OBJECTIVE";
    private String objective;
    private FirebaseFirestore db;
    Objective obj;
    Button ButtonEvent;
    TextView ObjectiveName;
    TextView ObjectiveDescription;
    TextView ObjectiveHistory;
    TextView ObjectiveProgram;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_description);

        ObjectiveName = findViewById(R.id.objective_name);
        ObjectiveDescription = findViewById(R.id.description_text);
        ObjectiveHistory = findViewById(R.id.history_text);
        ObjectiveProgram = findViewById(R.id.program_text);
        ButtonEvent = findViewById(R.id.eventbuton);

        db = FirebaseFirestore.getInstance();

        objective = getIntent().getStringExtra(ScanFragment.MainDescription);
        ObjectiveName.setText(objective);

        ButtonEvent.setText("See the events for " + objective);

        FindDescription();
    }

    public void SeeEvents(View view)
    {
        Intent intent = new Intent(this, EventsForObjectives.class);
        intent.putExtra(EventsForObjective, objective);
        startActivity(intent);
    }

    public void FindDescription()
    {
        DocumentReference docRef = db.collection("descriptions").document(objective);
        docRef.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                if (task.isSuccessful()) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()) {
                        Log.d(TAG, "DocumentSnapshot data: " + document.getData());
                        obj = document.toObject(Objective.class);
                        ObjectiveDescription.setText(obj.getDescription());
                        ObjectiveHistory.setText(obj.getHistory());
                        ObjectiveProgram.setText(obj.getProgram());
                    } else {
                        ObjectiveDescription.setText("Our application does not represent this objective.");
                        Log.d(TAG, "No such document");
                    }
                } else {
                    Log.d(TAG, "get failed with ", task.getException());
                }
            }
        });
    }
}
