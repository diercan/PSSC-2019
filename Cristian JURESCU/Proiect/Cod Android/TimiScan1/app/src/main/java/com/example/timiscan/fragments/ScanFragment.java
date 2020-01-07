package com.example.timiscan.fragments;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import com.example.timiscan.DescriptionActivity;
import com.example.timiscan.R;
import com.google.firebase.FirebaseApp;
import com.google.zxing.Result;

import me.dm7.barcodescanner.zxing.ZXingScannerView;


public class ScanFragment extends Fragment implements ZXingScannerView.ResultHandler {

    public static final String MainDescription = "MAIN_DESCRIPTION";
    private ZXingScannerView scannerView;
    private TextView txtResult;
    ImageButton imageButton;
    public AppCompatActivity activity;
    CharSequence InfoText = "Scan the qr code to find more obout the objective around you!";
    View sView;


    public ScanFragment() {
        // Required empty public constructor

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        FirebaseApp.initializeApp(getActivity());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        sView = inflater.inflate(R.layout.fragment_scan_fragment, container, false);

        FirebaseApp.initializeApp(getActivity());
        //Init
        scannerView = sView.findViewById(R.id.zxscan);
        txtResult = sView.findViewById(R.id.txt_result);
        imageButton = sView.findViewById(R.id.imageButtonScan);
        scannerView.setResultHandler(this);
        scannerView.startCamera();

        imageButton.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                Toast.makeText(getActivity(), InfoText, Toast.LENGTH_LONG).show();

            }
        });

        // Inflate the layout for this fragment
        return sView;
    }

    @Override
    public void onDestroy() {
        scannerView.stopCamera();
        super.onDestroy();
    }

    @Override
    public void onResume() {
        scannerView.startCamera();
        super.onResume();
    }

    @Override
    public void handleResult(Result rawResult)
    {
        txtResult.setText(rawResult.getText());

        Intent intent = new Intent(getActivity(), DescriptionActivity.class);
        intent.putExtra(MainDescription, rawResult.getText());
        startActivity(intent);

        scannerView.startCamera();
    }
}