package com.example.timiscan.fragments;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.timiscan.CreateEvent;
import com.example.timiscan.DescriptionActivity;
import com.example.timiscan.Events;
import com.example.timiscan.R;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.FirebaseApp;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

import java.util.ArrayList;


public class EventFragment extends Fragment implements View.OnClickListener{

    private RecyclerView recyclerView;
    private RecyclerView.Adapter mAdapter;
    private static final String TAG = "DocSnippets";
    private FirebaseFirestore db;
    private Button createButton;
    private ImageButton imageButton;
    private CharSequence InfoText = "Have a look above all events in town!";
    public final ArrayList<Events> myDataset = new ArrayList<>();

    public EventFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View eView = inflater.inflate(R.layout.fragment_event_fragment, container, false);

        db = FirebaseFirestore.getInstance();
        recyclerView = eView.findViewById(R.id.recyclerview);
        imageButton = eView.findViewById(R.id.imageButtonEvent);
        createButton = eView.findViewById(R.id.createButton);

        createButton.setOnClickListener(this);

        //Set the Info Button
        imageButton.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                Toast.makeText(getActivity(), InfoText, Toast.LENGTH_LONG).show();
            }
        });

        // use this setting to improve performance if you know that changes
        // in content do not change the layout size of the RecyclerView
        recyclerView.setHasFixedSize(true);

        // use a linear layout manager
        RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(getActivity());
        recyclerView.setLayoutManager(layoutManager);

        //Extract the Events from DataBase
        GetAllEvents();

        RecyclerView.ItemDecoration divider = new DividerItemDecoration(getActivity(), DividerItemDecoration.VERTICAL);
        RecyclerView.ItemDecoration divider1 = new DividerItemDecoration(getActivity(), DividerItemDecoration.HORIZONTAL);
        recyclerView.addItemDecoration(divider);
        recyclerView.addItemDecoration(divider1);


        // Inflate the layout for this fragment
        return eView;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        FirebaseApp.initializeApp(getActivity());
    }

    private void GetAllEvents()
    {
        final ArrayList<Events> myDataset = new ArrayList<>();
        db.collection("events")
                .get()
                .addOnCompleteListener(new OnCompleteListener<QuerySnapshot>() {
                    @Override
                    public void onComplete(@NonNull Task<QuerySnapshot> task) {
                        if (task.isSuccessful()) {
                            for (QueryDocumentSnapshot document : task.getResult()) {
                                Log.d(TAG, document.getId() + " => " + document.getData());
                                myDataset.add(document.toObject(Events.class));
                            }
                            mAdapter = new MyAdapter(getContext(), myDataset);
                            recyclerView.setAdapter(mAdapter);
                        } else {
                            Log.d(TAG, "Error getting documents: ", task.getException());
                        }
                    }
                });
    }

    @Override
    public void onClick(View v) {
        Intent intent = new Intent(getContext(), CreateEvent.class);
        startActivity(intent);
    }
}