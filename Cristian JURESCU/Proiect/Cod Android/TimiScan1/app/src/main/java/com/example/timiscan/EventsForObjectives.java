package com.example.timiscan;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;

import com.example.timiscan.fragments.MyAdapter;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.FirebaseApp;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

import java.util.ArrayList;

public class EventsForObjectives extends AppCompatActivity {

    private FirebaseFirestore db;
    private static final String TAG = "DocSnippets";
    private RecyclerView recyclerView;
    private RecyclerView.Adapter mAdapter;

    String objective;
    TextView title;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_events_for_objectives);
        FirebaseApp.initializeApp(this);

        db = FirebaseFirestore.getInstance();
        title = findViewById(R.id.objective_title);
        recyclerView = findViewById(R.id.recyclerview1);

        recyclerView.setHasFixedSize(true);
        RecyclerView.LayoutManager layoutManager1 = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(layoutManager1);

        RecyclerView.ItemDecoration divider = new DividerItemDecoration(this, DividerItemDecoration.VERTICAL);
        RecyclerView.ItemDecoration divider1 = new DividerItemDecoration(this, DividerItemDecoration.HORIZONTAL);
        recyclerView.addItemDecoration(divider);
        recyclerView.addItemDecoration(divider1);

        objective = getIntent().getStringExtra(DescriptionActivity.EventsForObjective);


        GetSpecificEvents();
    }

    private void GetSpecificEvents()
    {
        final ArrayList<Events> myDataset = new ArrayList<>();
        db.collection("events")
                .whereEqualTo("location", objective)
                .get()
                .addOnCompleteListener(new OnCompleteListener<QuerySnapshot>() {
                    @Override
                    public void onComplete(@NonNull Task<QuerySnapshot> task) {
                        if (task.isSuccessful()) {
                            title.setText("Events for " + objective);
                            for (QueryDocumentSnapshot document : task.getResult()) {
                                Log.d(TAG, document.getId() + " => " + document.getData());
                                myDataset.add(document.toObject(Events.class));
                            }
                            if(myDataset.isEmpty())
                            {
                                title.setText("There are no events for " + objective);
                            }
                            mAdapter = new MyAdapter(EventsForObjectives.this, myDataset);
                            recyclerView.setAdapter(mAdapter);
                        } else {
                            title.setText("There are no events for " + objective);
                            Log.d(TAG, "Error getting documents: ", task.getException());
                        }
                    }
                });
    }
}
