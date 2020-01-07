package com.example.timiscan;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;

import com.example.timiscan.fragments.EventFragment;
import com.example.timiscan.fragments.MapFragment;
import com.example.timiscan.fragments.ScanFragment;

public class PagerController extends FragmentPagerAdapter {
    int tabCounts;

    public PagerController(@NonNull FragmentManager fm, int tabCounts) {
        super(fm);
        this.tabCounts = tabCounts;
    }

    @NonNull
    @Override
    public Fragment getItem(int position) {
        switch (position) {
            case 0:
                return new ScanFragment();
            case 1:
                return new MapFragment();
            case 2:
                return new EventFragment();
            default:
                return null;

        }
    }

    @Override
    public int getCount() {
        return tabCounts;
    }
}
