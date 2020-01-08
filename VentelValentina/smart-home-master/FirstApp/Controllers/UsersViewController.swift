//
//  UsersViewController.swift
//  FirstApp
//
//  Created by Valentina Vențel on 11/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit

class UsersViewController: UIViewController, UserModelProtocol {

    var feedItems: NSArray = NSArray()
    var selectedUser: User = User()
    
    @IBOutlet weak var listTableView: UITableView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        title = "Users"
        
        listTableView.delegate = self
        listTableView.dataSource = self
        
        let homeModel = HomeModel()
        homeModel.delegate = self
        homeModel.downloadItems()
        // Do any additional setup after loading the view.
        
//        UIDevice.current.identifierForVendor!.uuidString
//        UIDevice.current.identifierForVendor
//        UIDevice.current.identifierForVendor!.uuidString
        
    }
    
    
    // MARK: HomeModelProtocol
    func itemsDownloaded(items: NSArray) {
        feedItems = items
        listTableView.reloadData()
    }

}

extension UsersViewController: UITableViewDataSource {
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let myCell = UITableViewCell(style: UITableViewCell.CellStyle.subtitle,
                                     reuseIdentifier: "cellId")
        let item: User = feedItems[indexPath.row] as! User
        myCell.textLabel!.text = item.fullName
        myCell.detailTextLabel?.text = item.description
        
        return myCell
    }
    
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return feedItems.count
    }
}

extension UsersViewController: UITableViewDelegate {
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        guard let item = feedItems[indexPath.row] as? User else {
            return
        }
        
        let alert = UIAlertController(title: nil,
                                      message: item.fullName,
                                      preferredStyle: .alert)
        
        let okAction = UIAlertAction(title: "Bine, boss!",
                                     style: .default) { (action) in
                                        //self.dismiss(animated: true,
                                        //             completion: nil)
        }
        alert.addAction(okAction)
        present(alert, animated: true)
    }
}
        
