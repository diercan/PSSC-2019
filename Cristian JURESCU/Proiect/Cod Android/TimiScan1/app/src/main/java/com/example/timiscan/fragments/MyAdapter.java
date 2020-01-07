package com.example.timiscan.fragments;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable;
import android.os.Build;
import android.transition.AutoTransition;
import android.transition.TransitionManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.RequiresApi;
import androidx.cardview.widget.CardView;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.recyclerview.widget.RecyclerView;

import com.example.timiscan.Events;
import com.example.timiscan.R;

import java.util.ArrayList;

public class MyAdapter extends RecyclerView.Adapter<MyAdapter.MyViewHolder> {
    private ArrayList<Events> mDataset;

    private Context context;
    ConstraintLayout expandableView;
    Button arrowBtn;
    CardView cardView;


    // Provide a reference to the views for each data item
    // Complex data items may need more than one view per item, and
    // you provide access to all the views for a data item in a view holder
    public static class MyViewHolder extends RecyclerView.ViewHolder{

        //private final CardView cardView;
        // each data item is just a string in this case
        public LinearLayout linear;
        @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
        public MyViewHolder(LinearLayout v) {
            super(v);
            linear = v;

            GradientDrawable gd = new GradientDrawable();
            gd.setCornerRadius(20);
            gd.setColor(Color.parseColor("#f7f7f7"));
            v.setBackground(gd);
        }
    }

    // Provide a suitable constructor (depends on the kind of dataset)
    public MyAdapter(Context context, ArrayList<Events> myDataset) {
        this.context = context;
        mDataset = myDataset;
    }

    // Create new views (invoked by the layout manager)
    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public MyAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent,
                                                     int viewType) {
        // create a new view
        LinearLayout v = (LinearLayout) LayoutInflater.from(parent.getContext())
                .inflate(R.layout.eveniment, parent, false);

        MyViewHolder vh = new MyViewHolder(v);
        return vh;
    }

    // Replace the contents of a view (invoked by the layout manager)
    @Override
    public void onBindViewHolder(MyViewHolder holder, final int position) {
        // - get element from your dataset at this position
        // - replace the contents of the view with that element
        TextView tv1 = holder.linear.findViewById(R.id.nume_even);
        TextView tv2 = holder.linear.findViewById(R.id.data_even);
        TextView tv3 = holder.linear.findViewById(R.id.locatie_even);
        TextView tv4 = holder.linear.findViewById(R.id.type_even);
        TextView tv5 = holder.linear.findViewById(R.id.description_even);

        expandableView = holder.linear.findViewById(R.id.expandableView);
        arrowBtn = holder.linear.findViewById(R.id.scrollIndicatorDown);
        cardView = holder.linear.findViewById(R.id.card_view);

        expandableView.setTag(position);

        tv1.setText(mDataset.get(position).getName());
        tv2.setText(mDataset.get(position).getData());
        tv3.setText(mDataset.get(position).getLocation());
        tv4.setText(mDataset.get(position).getType());
        tv5.setText(mDataset.get(position).getDescription());

        /*
        holder.cardView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //implement onClick
                Log.e("AICI", "CLICK BITCH" + mDataset.get(position).getName());

                Intent intent = new Intent(context, EventActivity.class);
                intent.putExtra(EventDescription, mDataset.get(position).getName());
                context.startActivity(intent);
            }
        });
        */


        arrowBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.e("AICI", "CLICK BITCH" + mDataset.get(position).getName());


                if(expandableView.getVisibility() == View.GONE)
                {
                    TransitionManager.beginDelayedTransition(cardView, new AutoTransition());
                    expandableView.setVisibility(View.VISIBLE);
                    arrowBtn.setBackgroundResource(R.drawable.arrow_down);
                }
                else
                {
                    TransitionManager.beginDelayedTransition(cardView, new AutoTransition());
                    expandableView.setVisibility(View.GONE);
                    arrowBtn.setBackgroundResource(R.drawable.up_arrow);
                }
            }
        });

    }

    // Return the size of your dataset (invoked by the layout manager)
    @Override
    public int getItemCount() {
        return mDataset.size();
    }
}

